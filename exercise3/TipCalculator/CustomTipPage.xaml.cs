namespace TipCalculator;

public partial class CustomTipPage : ContentPage
{
    private Color colorNavy = Colors.Navy;
    private Color colorSilver = Colors.Silver;
    private Color colorGray = Colors.Gray;
    private Color colorBlue = Colors.Blue;
    private Color colorWhite = Colors.White;

    public CustomTipPage()
    {
        InitializeComponent();

        billInput.TextChanged += (s, e) => CalculateTip(false, false);
        roundDown.Clicked += (s, e) => CalculateTip(false, true);
        roundUp.Clicked += (s, e) => CalculateTip(true, false);

        tipPercentSlider.ValueChanged += (s, e) =>
        {
            double pct = Math.Round(e.NewValue);
            tipPercent.Text = $"{pct}%";
            CalculateTip(false, false);
        };
    }

    void CalculateTip(bool roundUp, bool roundDown)
    {
        double t;
        if (Double.TryParse(billInput.Text, out t) && t > 0)
        {
            double pct = Math.Round(tipPercentSlider.Value);
            double tip = Math.Round(t * (pct / 100.0), 2);

            double final = t + tip;

            if (roundUp)
            {
                final = Math.Ceiling(final);
                tip = final - t;
            }
            else if (roundDown)
            {
                final = Math.Floor(final);
                tip = final - t;
            }

            tipOutput.Text = tip.ToString("C");
            totalOutput.Text = final.ToString("C");
        }
    }

    void OnDarkModeToggled(object sender, ToggledEventArgs e)
    {
        if (e.Value) // Dark: fondo azul oscuro, botones azules, texto botones azul
        {
            Resources["bgColor"] = colorNavy;
            Resources["fgColor"] = colorSilver;
            Resources["btnTextColor"] = colorBlue;
            Resources["btnCustomCalc"] = colorSilver;
        }
        else // Light: fondo plateado, botones azules, texto botones blanco
        {
            Resources["bgColor"] = colorSilver;
            Resources["fgColor"] = colorNavy;
            Resources["btnTextColor"] = colorWhite;
            Resources["btnCustomCalc"] = colorBlue;
        }
    }

    void OnNormalTip(object sender, EventArgs e) { tipPercentSlider.Value = 15; }
    void OnGenerousTip(object sender, EventArgs e) { tipPercentSlider.Value = 20; }
}