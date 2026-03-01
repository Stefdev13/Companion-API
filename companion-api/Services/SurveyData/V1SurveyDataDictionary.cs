
public class V1SurveyDataDictionary : ISurveyDataDictionary
{
    public List<string> getSurveyDataKeys(string route, string subQuestionKey, string dynamicParams, string country, string? region)
    {
        throw new NotImplementedException();
    }

    public Dictionary<string, object> getSurveyDataPoint(string route, string subQuestionKey, string dynamicParams, string country, string? region)
    {
        throw new NotImplementedException();
    }

    public List<QuestionOption> generateDynamicQuestionOptions(List<string> dynamicParams, string country, string? region)
    {
        throw new NotImplementedException();
    }

    public static readonly Dictionary<string, object> SurveyData = new Dictionary<string, object>()
    {
        //Car grams of co2e/km
        { $"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_co2e_rating}", new Dictionary<string, string>() {
            {"diesel-small", "139.3" },
            {"diesel-medium", "167.1" },
            {"diesel-large", "208.5" },
            {"petrol-small", "140.8" },
            {"petrol-medium", "178.2" },
            {"petrol-large", "272.2" },
            {"hybrid-small", "101.5" },
            {"hybrid-medium", "109" },
            {"hybrid-large", "152.4" },
            {"plug-in-hybrid-small", "54" },
            {"plug-in-hybrid-medium", "85" },
            {"plug-in-hybrid-large", "101.6" },
        } },
        //Car battery size
        { $"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_battery_size}", new Dictionary<string, string>() {
            {"plug-in-hybrid-small", "12" },
            {"plug-in-hybrid-medium", "18" },
            {"plug-in-hybrid-large", "30" },
            {"electric-small", "35" },
            {"electric-medium", "55" },
            {"electric-large", "75" },
        } },
        //Motorbike grams of co2e/km
        { $"mobility/motorbikes/{V1SubQuestionKeys.mobility_vehicle_co2e_rating}", new Dictionary<string, string>() {
            { "gasoline-small", "83.2"},
            { "gasoline-medium", "101.1"},
            { "gasoline-large", "132.5"},
        }},
        //Motorbike battery size
        { $"mobility/motorbikes/{V1SubQuestionKeys.mobility_vehicle_battery_size}", new Dictionary<string, string>() {
            { "electric-small", "1.4"},
            { "electric-medium", "5.8"},
            { "electric-large", "14.5"},
        }},
        //Motorbike battery size
        { $"mobility/motorbikes/{V1SubQuestionKeys.mobility_vehicle_battery_size}", new Dictionary<string, string>() {
            { "electric-small", "1.4"},
            { "electric-medium", "5.8"},
            { "electric-large", "14.5"},
        }},
        //E-Bike battery size
        { $"mobility/bikes/{V1SubQuestionKeys.mobility_vehicle_battery_size}", "0.65"},
        //TODO - Average values:
        //Food portion size & portions/week:
        //  Meat, fish & tofu
        //  Dairy & eggs
        //  Fruits & vegetables
        //  Grain products
        //  Other foods

        //TODO - Dynamic QuestionOptions:
        //Bus QuestionOptions
        //Light rail QuestionOptions
        //Rail QuestionOptions
    };
}