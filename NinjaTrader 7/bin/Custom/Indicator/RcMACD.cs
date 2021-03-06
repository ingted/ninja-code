// 
// Copyright (C) 2006, NinjaTrader LLC <www.ninjatrader.com>.
// NinjaTrader reserves the right to modify or overwrite this NinjaScript component with each release.
//

#region Using declarations
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Xml.Serialization;
using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;
#endregion

// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
	/// <summary>
	/// The MACD (Moving Average Convergence/Divergence) is a trend following momentum indicator that shows the relationship between two moving averages of prices.
	/// </summary>
	[Description("The MACD (Moving Average Convergence/Divergence) is a trend following momentum indicator that shows the relationship between two moving averages of prices.")]
    public class RcMACD : Indicator
    {
		#region Variables
		private int					fast	= 5;
		private int					slow	= 20;
		private int					smooth	= 30;
		private	DataSeries		fastEma;
		private	DataSeries		slowEma;
		private int upDown = 0;
		#endregion

		/// <summary>
		/// This method is used to configure the indicator and is called once before any bar data is loaded.
		/// </summary>
		protected override void Initialize()
		{
			/*
			Add(new Plot(Color.Green, "Macd"));
			Add(new Plot(Color.DarkViolet, "Avg"));
			Add(new Plot(new Pen(Color.Navy, 2), PlotStyle.Bar, "Diff"));
			*/
            Add(new Plot(new Pen(Color.FromKnownColor(KnownColor.Black),2), PlotStyle.Bar, "Macd"));
            Add(new Plot(new Pen(Color.FromKnownColor(KnownColor.Blue), 3), PlotStyle.Line, "Avg"));
            Add(new Plot(new Pen(Color.FromKnownColor(KnownColor.Transparent), 1), PlotStyle.Line, "Diff"));
			Add(new Plot(new Pen(Color.FromKnownColor(KnownColor.Black), 1), PlotStyle.Line, "Mom"));
			Add(new Line(Color.DarkGray, 0, "Zero line"));

			fastEma	= new DataSeries(this);
			slowEma	= new DataSeries(this);
		}

		/// <summary>
		/// Calculates the indicator value(s) at the current index.
		/// </summary>
		protected override void OnBarUpdate()
		{
			if (CurrentBar == 0)
			{
				fastEma.Set(Input[0]);
				slowEma.Set(Input[0]);
				Value.Set(0);
				Avg.Set(0);
				Diff.Set(0);
			}
			else
			{
				upDown = 0;
				fastEma.Set((2.0 / (1 + Fast)) * Input[0] + (1 - (2.0 / (1 + Fast))) * fastEma[1]);
				slowEma.Set((2.0 / (1 + Slow)) * Input[0] + (1 - (2.0 / (1 + Slow))) * slowEma[1]);

				double macd		= fastEma[0] - slowEma[0];
				double macdAvg	= (2.0 / (1 + Smooth)) * macd + (1 - (2.0 / (1 + Smooth))) * Avg[1];
				
				Value.Set(macd);
				Avg.Set(macdAvg);
				//Diff.Set(macd - macdAvg);
				Diff.Set(macd);
				
				if (Math.Abs(Value[0]) > Math.Abs(Value[1]))
				{
					PlotColors[0][0] = Color.Green;
					upDown++;
				} else {
					PlotColors[0][0] = Color.Red;
					upDown--;
				}
				
				if (Rising(Avg)) 
				{
					PlotColors[1][0] = Color.Green;
					upDown++;
				} else {
					PlotColors[1][0] = Color.Red;
					upDown--;
				}
				// Sets the bar color to its default color as defined in the chart properties dialog
				//BarColor = Color.Empty;
				//CandleOutlineColorSeries[0] = Color.Black;	
				//CandleOutlineColor = Color.Black;
				if (macd > 0 && macd > macdAvg)
					BarColor = Color.Green;
				else if (macd < 0 && macd < macdAvg)
					BarColor = Color.Red;
				else
					BarColor = Color.Black;

				
				Mom.Set(macd);
			}
		}

		#region Properties
		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore()]
		public DataSeries Avg
		{
			get { return Values[1]; }
		}

		/// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore()]
		public DataSeries Default
		{
			get { return Values[0]; }
		}
		
        /// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore()]
		public DataSeries Diff
		{
			get { return Values[2]; }
		}

        /// <summary>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore()]
		public DataSeries Mom
		{
			get { return Values[3]; }
		}

		/// <summary>
		/// </summary>
		[Description("Number of bars for fast EMA")]
		[GridCategory("Parameters")]
		public int Fast
		{
			get { return fast; }
			set { fast = Math.Max(1, value); }
		}

		/// <summary>
		/// </summary>
		[Description("Number of bars for slow EMA")]
		[GridCategory("Parameters")]
		public int Slow
		{
			get { return slow; }
			set { slow = Math.Max(1, value); }
		}

		/// <summary>
		/// </summary>
		[Description("Number of bars for smoothing")]
		[GridCategory("Parameters")]
		public int Smooth
		{
			get { return smooth; }
			set { smooth = Math.Max(1, value); }
		}
		#endregion
	}
}


#region NinjaScript generated code. Neither change nor remove.
// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
    public partial class Indicator : IndicatorBase
    {
        private RcMACD[] cacheRcMACD = null;

        private static RcMACD checkRcMACD = new RcMACD();

        /// <summary>
        /// The MACD (Moving Average Convergence/Divergence) is a trend following momentum indicator that shows the relationship between two moving averages of prices.
        /// </summary>
        /// <returns></returns>
        public RcMACD RcMACD(int fast, int slow, int smooth)
        {
            return RcMACD(Input, fast, slow, smooth);
        }

        /// <summary>
        /// The MACD (Moving Average Convergence/Divergence) is a trend following momentum indicator that shows the relationship between two moving averages of prices.
        /// </summary>
        /// <returns></returns>
        public RcMACD RcMACD(Data.IDataSeries input, int fast, int slow, int smooth)
        {
            if (cacheRcMACD != null)
                for (int idx = 0; idx < cacheRcMACD.Length; idx++)
                    if (cacheRcMACD[idx].Fast == fast && cacheRcMACD[idx].Slow == slow && cacheRcMACD[idx].Smooth == smooth && cacheRcMACD[idx].EqualsInput(input))
                        return cacheRcMACD[idx];

            lock (checkRcMACD)
            {
                checkRcMACD.Fast = fast;
                fast = checkRcMACD.Fast;
                checkRcMACD.Slow = slow;
                slow = checkRcMACD.Slow;
                checkRcMACD.Smooth = smooth;
                smooth = checkRcMACD.Smooth;

                if (cacheRcMACD != null)
                    for (int idx = 0; idx < cacheRcMACD.Length; idx++)
                        if (cacheRcMACD[idx].Fast == fast && cacheRcMACD[idx].Slow == slow && cacheRcMACD[idx].Smooth == smooth && cacheRcMACD[idx].EqualsInput(input))
                            return cacheRcMACD[idx];

                RcMACD indicator = new RcMACD();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.Fast = fast;
                indicator.Slow = slow;
                indicator.Smooth = smooth;
                Indicators.Add(indicator);
                indicator.SetUp();

                RcMACD[] tmp = new RcMACD[cacheRcMACD == null ? 1 : cacheRcMACD.Length + 1];
                if (cacheRcMACD != null)
                    cacheRcMACD.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheRcMACD = tmp;
                return indicator;
            }
        }
    }
}

// This namespace holds all market analyzer column definitions and is required. Do not change it.
namespace NinjaTrader.MarketAnalyzer
{
    public partial class Column : ColumnBase
    {
        /// <summary>
        /// The MACD (Moving Average Convergence/Divergence) is a trend following momentum indicator that shows the relationship between two moving averages of prices.
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.RcMACD RcMACD(int fast, int slow, int smooth)
        {
            return _indicator.RcMACD(Input, fast, slow, smooth);
        }

        /// <summary>
        /// The MACD (Moving Average Convergence/Divergence) is a trend following momentum indicator that shows the relationship between two moving averages of prices.
        /// </summary>
        /// <returns></returns>
        public Indicator.RcMACD RcMACD(Data.IDataSeries input, int fast, int slow, int smooth)
        {
            return _indicator.RcMACD(input, fast, slow, smooth);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// The MACD (Moving Average Convergence/Divergence) is a trend following momentum indicator that shows the relationship between two moving averages of prices.
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.RcMACD RcMACD(int fast, int slow, int smooth)
        {
            return _indicator.RcMACD(Input, fast, slow, smooth);
        }

        /// <summary>
        /// The MACD (Moving Average Convergence/Divergence) is a trend following momentum indicator that shows the relationship between two moving averages of prices.
        /// </summary>
        /// <returns></returns>
        public Indicator.RcMACD RcMACD(Data.IDataSeries input, int fast, int slow, int smooth)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.RcMACD(input, fast, slow, smooth);
        }
    }
}
#endregion
