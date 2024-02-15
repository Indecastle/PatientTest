namespace PatientTest;

public static class JsonSetup
{
    public static IMvcBuilder AddAppJsonSettings(this IMvcBuilder mvcBuilder)
    {
        return mvcBuilder.AddJsonOptions(x =>
            x.JsonSerializerOptions.Converters.Add(new StringConvertableJsonConverterFactory()));
    }
}