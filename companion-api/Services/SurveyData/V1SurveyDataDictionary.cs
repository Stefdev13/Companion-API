
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
        //Food portion size
        { $"food/meat-fish-tofu/beef/{V1SubQuestionKeys.food_portion_size}", "0.15"},
        { $"food/meat-fish-tofu/lamb-mutton/{V1SubQuestionKeys.food_portion_size}", "0.15"},
        { $"food/meat-fish-tofu/pork/{V1SubQuestionKeys.food_portion_size}", "0.15"},
        { $"food/meat-fish-tofu/poultry/{V1SubQuestionKeys.food_portion_size}", "0.15"},
        { $"food/meat-fish-tofu/shellfish-farmed/{V1SubQuestionKeys.food_portion_size}", "0.15"},
        { $"food/meat-fish-tofu/shellfish-wild/{V1SubQuestionKeys.food_portion_size}", "0.15"},
        { $"food/meat-fish-tofu/fish-farmed/{V1SubQuestionKeys.food_portion_size}", "0.15"},
        { $"food/meat-fish-tofu/fish-wild/{V1SubQuestionKeys.food_portion_size}", "0.15"},
        { $"food/meat-fish-tofu/tofu/{V1SubQuestionKeys.food_portion_size}", "0.15"},
        { $"food/meat-fish-tofu/seitan/{V1SubQuestionKeys.food_portion_size}", "0.15"},
        { $"food/meat-fish-tofu/seitan/{V1SubQuestionKeys.food_portion_size}", "0.15"},
        { $"food/dairy-eggs/cheese/{V1SubQuestionKeys.food_portion_size}", "0.15"},
        { $"food/dairy-eggs/milk/{V1SubQuestionKeys.food_portion_size}", "0.25"},
        { $"food/dairy-eggs/cream/{V1SubQuestionKeys.food_portion_size}", "0.1"},
        { $"food/dairy-eggs/butter/{V1SubQuestionKeys.food_portion_size}", "0.01"},
        { $"food/dairy-eggs/soymilk/{V1SubQuestionKeys.food_portion_size}", "0.25"},
        { $"food/dairy-eggs/eggs/{V1SubQuestionKeys.food_portion_size}", "2"},
        { $"food/fruits-vegetables/citrus-fruits/{V1SubQuestionKeys.food_portion_size}", "1"},
        { $"food/fruits-vegetables/berries-grapes/{V1SubQuestionKeys.food_portion_size}", "100"},
        { $"food/fruits-vegetables/bananas/{V1SubQuestionKeys.food_portion_size}", "1"},
        { $"food/fruits-vegetables/other-fruit/{V1SubQuestionKeys.food_portion_size}", "100"},
        { $"food/fruits-vegetables/pulses/{V1SubQuestionKeys.food_portion_size}", "100"},
        { $"food/fruits-vegetables/peas/{V1SubQuestionKeys.food_portion_size}", "100"},
        { $"food/fruits-vegetables/potatoes/{V1SubQuestionKeys.food_portion_size}", "200"},
        { $"food/fruits-vegetables/unions-leeks/{V1SubQuestionKeys.food_portion_size}", "50"},
        { $"food/fruits-vegetables/root-vegetables/{V1SubQuestionKeys.food_portion_size}", "200"},
        { $"food/fruits-vegetables/tomatoes/{V1SubQuestionKeys.food_portion_size}", "100"},
        { $"food/fruits-vegetables/corn/{V1SubQuestionKeys.food_portion_size}", "100"},
        { $"food/fruits-vegetables/other-vegetables/{V1SubQuestionKeys.food_portion_size}", "100"},
        { $"food/grain-products/rice/{V1SubQuestionKeys.food_portion_size}", "80"},
        { $"food/grain-products/pasta/{V1SubQuestionKeys.food_portion_size}", "80"},
        { $"food/grain-products/bread/{V1SubQuestionKeys.food_portion_size}", "100"},
        { $"food/grain-products/oatmeal/{V1SubQuestionKeys.food_portion_size}", "50"},
        { $"food/grain-products/cereals/{V1SubQuestionKeys.food_portion_size}", "50"},
        { $"food/other-foods/chocolate/{V1SubQuestionKeys.food_portion_size}", "50"},
        { $"food/other-foods/coffee/{V1SubQuestionKeys.food_portion_size}", "250"},
        { $"food/other-foods/groundnuts/{V1SubQuestionKeys.food_portion_size}", "30"},
        { $"food/other-foods/other-nuts/{V1SubQuestionKeys.food_portion_size}", "30"},
        { $"food/other-foods/olive-oil/{V1SubQuestionKeys.food_portion_size}", "10"},
        { $"food/other-foods/other-oil/{V1SubQuestionKeys.food_portion_size}", "10"},
        { $"food/other-foods/wine/{V1SubQuestionKeys.food_portion_size}", "150"},
        { $"food/other-foods/beer/{V1SubQuestionKeys.food_portion_size}", "250"},
        { $"food/other-foods/sugar/{V1SubQuestionKeys.food_portion_size}", "10"},

        //TODO - Dynamic QuestionOptions:
        //Bus QuestionOptions
        //Light rail QuestionOptions
        //Rail QuestionOptions
    };
}