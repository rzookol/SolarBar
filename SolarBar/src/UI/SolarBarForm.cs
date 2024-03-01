using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using SolarBar.Config;
using SolarBar.Services;
using SolarBar.Model;
using SolarBar.Extensions;
using System.Linq;
using SolarBar.Model.Dto;
using System.IO;
using System.Text;

namespace SolarBar.UI
{
    public partial class UcSolarBar : UserControl
    {
        private const int V = 1000*60*15; // 15 mins
        private readonly int _runnerTimerIntervalMax = V;
        private readonly Timer refreshTimer;
        private ShowMode mode = ShowMode.Day;
        private IProxyService _proxyService;
        Overview overview = null;
        SiteDetailsDto details = null;
        BarConfig config;
        int currentMonth = 0;
        int currentDay   = 0;
        int currentYear  = 0;
        Dictionary<ShowMode, Dictionary<int, Energy>> mapEnergy = 
            new Dictionary<ShowMode, Dictionary<int,Energy>>();

        public UcSolarBar()
        {
            InitializeComponent();

            refreshTimer = new Timer
            {
                Interval = _runnerTimerIntervalMax
            };
            mapEnergy[ShowMode.Month] = new Dictionary<int, Energy>();
            mapEnergy[ShowMode.Year] = new Dictionary<int, Energy>();
            mapEnergy[ShowMode.Day] = new Dictionary<int, Energy>();
            mapEnergy[ShowMode.Total] = new Dictionary<int, Energy>();

            refreshTimer.Tick += refreshTimer_Tick;
            tooltip1.SetToolTip(this.energyInfo, "Czekam na dane...");
        }

        public void Run(IProxyService proxyService, BarConfig config)
        {
            this.config = config;
            _proxyService = proxyService;
            refreshTimer.Start();

            overview = _proxyService.GetOverviewAsync().Result;
            energyInfo.Text = overview?.ShowData(mode) ?? "";
            details = _proxyService.GetSiteDetailsAsync().Result;
            tooltip1.SetToolTip(this.energyInfo, details?.details.ToolTipInfo());
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Hour >= config.ChartStartHour && DateTime.Now.Hour <= config.ChartEndHour)
            { 
                overview = _proxyService.GetOverviewAsync().Result;
                energyInfo.Text = overview?.ShowData(mode) ?? "";
            }
        }

        private Energy RefreshChart()
        {
            Energy energy = null;
            if (overview==null)
            {
                overview = _proxyService.GetOverviewAsync().Result;
                energyInfo.Text = overview?.ShowData(mode) ?? "";
            }

            switch (mode)
            {
                case ShowMode.Total:
                    if (mapEnergy.ContainsKey(ShowMode.Total) &&
                        mapEnergy[ShowMode.Total].ContainsKey(0))
                    {
                        energy = mapEnergy[ShowMode.Total][0];
                    }
                    else
                    {
                        energy = _proxyService.GetEnergyWholeTimeAsync().Result;
                        if (energy != null) mapEnergy[ShowMode.Total][0] = energy;
                    }
                    break;
                case ShowMode.Day:
                    if (mapEnergy.ContainsKey(ShowMode.Day) && 
                        mapEnergy[ShowMode.Day].ContainsKey(currentDay) &&
                        currentDay != 0)
                    {
                        energy = mapEnergy[ShowMode.Day][currentDay];
                    }
                    else
                    {
                        energy = _proxyService.GetEnergyTodayAsync(currentDay).Result;
                        energy?.FilterValuesByNull(config.ChartStartHour, config.ChartEndHour);
                        if (energy!= null) mapEnergy[ShowMode.Day][currentDay] = energy;
                    }

                    if (energy != null && overview != null)
                    {
                        overview.LastDayData = energy.SumValues();
                    }
                    break;
                case ShowMode.Current:
                    energy = _proxyService.GetEnergyTodayAsync(0).Result;
                    energy?.FilterValuesByNull(config.ChartStartHour, config.ChartEndHour);
                    break;
                case ShowMode.Month:
                    if (mapEnergy.ContainsKey(ShowMode.Month) &&
                        mapEnergy[ShowMode.Month].ContainsKey(currentMonth) &&
                        currentMonth != 0)
                    {
                        energy = mapEnergy[ShowMode.Month][currentMonth];
                    }
                    else
                    {
                        energy = _proxyService.GetEnergyThisMonthAsync(currentMonth).Result;
                        if (energy!= null) mapEnergy[ShowMode.Month][currentMonth] = energy;
                    }

                    if (energy != null && overview != null)
                    {
                        overview.LastMonthData = energy.SumValues();
                    }
                    break;
                case ShowMode.Year:
                    if (mapEnergy.ContainsKey(ShowMode.Year) && 
                        mapEnergy[ShowMode.Year].ContainsKey(currentYear) &&
                        currentYear != 0)
                    {
                        energy = mapEnergy[ShowMode.Year][currentYear];
                    }
                    else
                    {
                        energy = _proxyService.GetEnergyThisYearAsync().Result;
                        if (energy !=null)
                        { 
                            MeasuredValue[] array = new MeasuredValue[12];
                            for (int i = 0; i < array.Length; i++)
                            {
                                array[i] = new MeasuredValue
                                {
                                    date = DateTime.Now,
                                    value = 0
                                };
                            }

                            Array.Copy(energy.Values, array, energy.Values.Length);
                            energy.Values = array;
                            mapEnergy[ShowMode.Year][currentYear] = energy;
                        }
                    }
                break;
            }

            if (energy!=null && energy.Values != null)
            {
                barChart1.RefreshData(energy.Values.Select(v => v.value ?? 0.0f).ToList());
                energyDate.Text = energy.ShowTimeRange(mode);
            }

            return energy;
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseEventArgs = e as MouseEventArgs;
            if (mouseEventArgs != null &&
                mouseEventArgs.Button == MouseButtons.Left)
            {
                mode = mode.NextMode();
                energyInfo.Text = overview?.ShowData(mode) ?? "";
                currentMonth = 0;
                currentDay = 0;
                RefreshChart();
            }
        }

        private void showEditorMenuItem_Click(object sender, EventArgs e)
        {
            ShowEditor();
        }

        private void buttonL_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseEventArgs = e as MouseEventArgs;
            if (mouseEventArgs != null &&
                mouseEventArgs.Button == MouseButtons.Left)
            {
                    currentDay--;
                    currentMonth--;
                    currentYear--;
                    RefreshChart();
                    energyInfo.Text = overview?.ShowData(mode) ?? "";
            }
        }

        private void buttonP_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseEventArgs = e as MouseEventArgs;
            if (mouseEventArgs != null &&
                mouseEventArgs.Button == MouseButtons.Left)
            {
                bool refresh = true;
                if (mode == ShowMode.Day)
                {
                    currentDay++;

                    if (currentDay > 0)
                    {
                        currentDay = 0;
                        refresh = false;
                    }
                }
                else if (mode == ShowMode.Month)
                {
                    currentMonth++;

                    if (currentMonth > 0)
                    {
                        currentMonth = 0;
                        refresh = false;
                    }
                }
                else if (mode == ShowMode.Year)
                {
                    currentYear++;

                    if (currentYear > 0)
                    {
                        currentYear = 0;
                        refresh = false;
                    }
                }

                if (refresh)
                {
                    RefreshChart();
                    energyInfo.Text = overview?.ShowData(mode) ?? "";
                }
            }
        }

        private void ShowEditor()
        {
            try
            {
                Process.Start("notepad.exe", config.ConfigPath); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie można otworzyć pliku: " + ex.Message);
            }
        }

        private void SaveToCSVButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                    {
                        // Nagłówki kolumn
                        writer.WriteLine("TimeUnit,Unit,MeasuredBy,Date,Value");
                        Energy energy = RefreshChart();
                        // Zapisuj każdy pomiar oddzielony przecinkami
                        foreach (var measuredValue in energy.Values)
                        {
                            writer.WriteLine($"{energy.TimeUnit},{energy.Unit},{energy.MeasuredBy},{measuredValue.date},{measuredValue.value}");
                        }

                        MessageBox.Show("Dane zostały zapisane do pliku CSV.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd podczas zapisu pliku: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
