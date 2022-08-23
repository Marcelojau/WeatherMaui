using WeatherApp.Services;

namespace WeatherApp;

public partial class WeatherPage : ContentPage
{
	public List<Models.List> WeatherList;
	private double Latitude;
    private double Longitude;
    public WeatherPage()
	{
		InitializeComponent();
		WeatherList = new List<Models.List>();

    }

	protected async override void OnAppearing()
	{
		base.OnAppearing();
		await GetLocation();
        await GetWeatherDataByLocation(Latitude, Longitude);

    }

    public async Task GetLocation()
    {
        var location = await LocationService.GetLocation();
        Latitude = location.Latitude;
        Longitude = location.Longitude;
    }

    public async Task GetWeatherDataByLocation(double latitude, double longitude)
	{
        var result = await ApiService.GetWeather(Latitude, Longitude);
        UpdateUI(result);

    }

    public async Task GetWeatherDataByCity(string city)
    {
        var result = await ApiService.GetWeatherByCity(city);

        UpdateUI(result);
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var response = await DisplayPromptAsync("Search Location", "", placeholder: "Search weather by city", accept: "Search", cancel: "Cancel");
        if (response != null)
        {
            await GetWeatherDataByCity(response);
        }

    }

    private async void TapLocation_Tapped(object sender, EventArgs e)
    {
        await GetLocation();
        await GetWeatherDataByLocation(Latitude, Longitude);
    }


    public void UpdateUI(dynamic result)
    {
        foreach (var item in result.list)
        {
            WeatherList.Add(item);
        }

        CvWeather.ItemsSource = WeatherList;

        LblCity.Text = result.city.name;
        LblWeatherDescription.Text = result.list[0].weather[0].description;
        LblTemperature.Text = result.list[0].main.temperature + "ºC";
        LblHumidity.Text = result.list[0].main.humidity + "%";
        LblWind.Text = result.list[0].wind.speed + "km/h";
        ImgWeatherIcon.Source = result.list[0].weather[0].customIcon;
    }
}