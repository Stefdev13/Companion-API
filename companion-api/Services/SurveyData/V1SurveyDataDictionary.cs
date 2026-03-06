
public class V1SurveyDataDictionary : ISurveyDataDictionary
{
    public List<string> getSurveyDataKeys(string route, string subQuestionKey, string dynamicParams, string country, string? region)
    {
        throw new NotImplementedException();
    }

    public Dictionary<string, string>? getSurveyDataPoint(string route, string subQuestionKey, List<string>? dynamicParams, string? country, string? region)
    {
        string dynamicParamsAsString = dynamicParams != null && dynamicParams.Count > 0 ? $"/{string.Join("/", dynamicParams)}" : "";

        string countryAndRegion = "";

        if (country != null)
        {
            countryAndRegion = country != null ? $"/{country}" : "";
            if (region != null)
            {
                countryAndRegion.Concat($"-{region}");
            }
        }

        Dictionary<string, string>? result;
        AverageValueSurveyData.TryGetValue($"{route}/{subQuestionKey}{dynamicParamsAsString}{countryAndRegion}", out result);

        return result;
    }

    public List<string> generateDynamicQuestionOptions(List<string> dynamicParams, string country, string? region)
    {
        string location = country + region ?? "";

        List<string>? result = null;

        if (DynamicQuestionOptions.TryGetValue($"{string.Join("/", dynamicParams)}", out var innerDict))
        {
            innerDict.TryGetValue(location, out result);
        }

        return result ?? [];
    }

    public static readonly Dictionary<string, Dictionary<string, string>> AverageValueSurveyData = new Dictionary<string, Dictionary<string, string>>()
    {
        //Car emissions figures
        {$"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_co2e_rating}/diesel-small", new Dictionary<string, string>()
        {
            {"value", "139.3"},
            {"unit", "g co2e/km"},
        }
        },
        {$"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_co2e_rating}/diesel-medium", new Dictionary<string, string>()
        {
            {"value", "167.1"},
            {"unit", "g co2e/km"},
        }
        },
        {$"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_co2e_rating}/diesel-large", new Dictionary<string, string>()
        {
            {"value", "208.5"},
            {"unit", "g co2e/km"},
        }
        },
        {$"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_co2e_rating}/petrol-small", new Dictionary<string, string>()
        {
            {"value", "140.8"},
            {"unit", "g co2e/km"},
        }
        },
        {$"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_co2e_rating}/petrol-medium", new Dictionary<string, string>()
        {
            {"value", "178.2"},
            {"unit", "g co2e/km"},
        }
        },
        {$"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_co2e_rating}/petrol-large", new Dictionary<string, string>()
        {
            {"value", "272.2"},
            {"unit", "g co2e/km"},
        }
        },
        {$"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_co2e_rating}/hybrid-small", new Dictionary<string, string>()
        {
            {"value", "101.5"},
            {"unit", "g co2e/km"},
        }
        },
        {$"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_co2e_rating}/hybrid-medium", new Dictionary<string, string>()
        {
            {"value", "109"},
            {"unit", "g co2e/km"},
        }
        },
        {$"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_co2e_rating}/hybrid-large", new Dictionary<string, string>()
        {
            {"value", "152.4"},
            {"unit", "g co2e/km"},
        }
        },
        {$"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_co2e_rating}/plug-in-hybrid-small", new Dictionary<string, string>()
        {
            {"value", "54"},
            {"unit", "g co2e/km"},
        }
        },
        {$"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_co2e_rating}/plug-in-hybrid-medium", new Dictionary<string, string>()
        {
            {"value", "85"},
            {"unit", "g co2e/km"},
        }
        },
        {$"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_co2e_rating}/plug-in-hybrid-large", new Dictionary<string, string>()
        {
            {"value", "101.6"},
            {"unit", "g co2e/km"},
        }
        },

        // Car battery size
        {$"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_battery_size}/plug-in-hybrid-small", new Dictionary<string, string>()
        {
            {"value", "12"},
            {"unit", "kWh"},
        }
        },
        {$"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_battery_size}/plug-in-hybrid-medium", new Dictionary<string, string>()
        {
            {"value", "18"},
            {"unit", "kWh"},
        }
        },
        {$"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_battery_size}/plug-in-hybrid-large", new Dictionary<string, string>()
        {
            {"value", "30"},
            {"unit", "kWh"},
        }
        },
        {$"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_battery_size}/electric-small", new Dictionary<string, string>()
        {
            {"value", "35"},
            {"unit", "kWh"},
        }
        },
        {$"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_battery_size}/electric-medium", new Dictionary<string, string>()
        {
            {"value", "55"},
            {"unit", "kWh"},
        }
        },
        {$"mobility/cars/{V1SubQuestionKeys.mobility_vehicle_battery_size}/electric-large", new Dictionary<string, string>()
        {
            {"value", "75"},
            {"unit", "kWh"},
        }
        },

        //Motorbike grams of co2e/km
        {$"mobility/motorbikes/{V1SubQuestionKeys.mobility_vehicle_co2e_rating}/gasoline-small", new Dictionary<string, string>()
        {
            {"value", "83.2"},
            {"unit", "g co2e/km"},
        }
        },
        {$"mobility/motorbikes/{V1SubQuestionKeys.mobility_vehicle_co2e_rating}/gasoline-medium", new Dictionary<string, string>()
        {
            {"value", "101.1"},
            {"unit", "g co2e/km"},
        }
        },
        {$"mobility/motorbikes/{V1SubQuestionKeys.mobility_vehicle_co2e_rating}/gasoline-large", new Dictionary<string, string>()
        {
            {"value", "132.5"},
            {"unit", "g co2e/km"},
        }
        },

        //Motorbike battery size
        {$"mobility/motorbikes/{V1SubQuestionKeys.mobility_vehicle_battery_size}/electric-small", new Dictionary<string, string>()
        {
            {"value", "1.4"},
            {"unit", "kWh"},
        }
        },
        {$"mobility/motorbikes/{V1SubQuestionKeys.mobility_vehicle_battery_size}/electric-medium", new Dictionary<string, string>()
        {
            {"value", "5.8"},
            {"unit", "kWh"},
        }
        },
        {$"mobility/motorbikes/{V1SubQuestionKeys.mobility_vehicle_battery_size}/electric-large", new Dictionary<string, string>()
        {
            {"value", "14.5"},
            {"unit", "kWh"},
        }
        },

        //E-Bike battery size
        {$"mobility/bikes/{V1SubQuestionKeys.mobility_vehicle_battery_size}", new Dictionary<string, string>()
        {
            {"value", "0.65"},
            {"unit", "kWh"},
        }
        },

        //Food portion size
        {$"food/meat-fish-tofu/beef/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "0.15"},
            {"unit", "kg"},
        }
        },
        {$"food/meat-fish-tofu/lamb-mutton/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "0.15"},
            {"unit", "kg"},
        }
        },
        {$"food/meat-fish-tofu/pork/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "0.15"},
            {"unit", "kg"},
        }
        },
        {$"food/meat-fish-tofu/poultry/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "0.15"},
            {"unit", "kg"},
        }
        },
        {$"food/meat-fish-tofu/shellfish-farmed/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "0.15"},
            {"unit", "kg"},
        }
        },
        {$"food/meat-fish-tofu/shellfish-wild/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "0.15"},
            {"unit", "kg"},
        }
        },
        {$"food/meat-fish-tofu/fish-farmed/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "0.15"},
            {"unit", "kg"},
        }
        },
        {$"food/meat-fish-tofu/fish-wild/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "0.15"},
            {"unit", "kg"},
        }
        },
        {$"food/meat-fish-tofu/tofu/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "0.15"},
            {"unit", "kg"},
        }
        },
        {$"food/meat-fish-tofu/seitan/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "0.15"},
            {"unit", "kg"},
        }
        },
        {$"food/dairy-eggs/cheese/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "0.15"},
            {"unit", "kg"},
        }
        },
        {$"food/dairy-eggs/milk/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "25"},
            {"unit", "cl"},
        }
        },
        {$"food/dairy-eggs/cream/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "10"},
            {"unit", "cl"},
        }
        },
        {$"food/dairy-eggs/butter/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "0.01"},
            {"unit", "kg"},
        }
        },
        {$"food/dairy-eggs/soymilk/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "25"},
            {"unit", "cl"},
        }
        },
        {$"food/dairy-eggs/eggs/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "2"},
            {"unit", "eggs"},
        }
        },
        {$"food/fruits-vegetables/citrus-fruits/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "1"},
            {"unit", "piece"},
        }
        },
        {$"food/fruits-vegetables/berries-grapes/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "100"},
            {"unit", "g"},
        }
        },
        {$"food/fruits-vegetables/bananas/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "1"},
            {"unit", "bananas"},
        }
        },
        {$"food/fruits-vegetables/other-fruit/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "100"},
            {"unit", "g"},
        }
        },
        {$"food/fruits-vegetables/pulses/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "100"},
            {"unit", "g"},
        }
        },
        {$"food/fruits-vegetables/peas/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "100"},
            {"unit", "g"},
        }
        },
        {$"food/fruits-vegetables/potatoes/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "200"},
            {"unit", "g"},
        }
        },
        {$"food/fruits-vegetables/unions-leeks/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "50"},
            {"unit", "g"},
        }
        },
        {$"food/fruits-vegetables/root-vegetables/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "200"},
            {"unit", "g"},
        }
        },
        {$"food/fruits-vegetables/tomatoes/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "100"},
            {"unit", "g"},
        }
        },
        {$"food/fruits-vegetables/corn/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "100"},
            {"unit", "g"},
        }
        },
        {$"food/fruits-vegetables/other-vegetables/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "100"},
            {"unit", "g"},
        }
        },
        {$"food/grain-products/rice/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "80"},
            {"unit", "g"},
        }
        },
        {$"food/grain-products/pasta/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "80"},
            {"unit", "g"},
        }
        },
        {$"food/grain-products/bread/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "100"},
            {"unit", "g"},
        }
        },
        {$"food/grain-products/oatmeal/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "50"},
            {"unit", "g"},
        }
        },
        {$"food/grain-products/cereals/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "50"},
            {"unit", "g"},
        }
        },
        {$"food/other-foods/chocolate/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "50"},
            {"unit", "g"},
        }
        },
        {$"food/other-foods/coffee/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "25"},
            {"unit", "cl"},
        }
        },
        {$"food/other-foods/groundnuts/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "30"},
            {"unit", "g"},
        }
        },
        {$"food/other-foods/other-nuts/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "30"},
            {"unit", "g"},
        }
        },
        {$"food/other-foods/olive-oil/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "10"},
            {"unit", "cl"},
        }
        },
        {$"food/other-foods/other-oil/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "10"},
            {"unit", "cl"},
        }
        },
        {$"food/other-foods/wine/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "150"},
            {"unit", "cl"},
        }
        },
        {$"food/other-foods/beer/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "250"},
            {"unit", "cl"},
        }
        },
        {$"food/other-foods/sugar/{V1SubQuestionKeys.food_portion_size}", new Dictionary<string, string>()
        {
            {"value", "10"},
            {"unit", "g"},
        }
        },
    };


    public static readonly Dictionary<string, Dictionary<string, List<string>>> DynamicQuestionOptions = new Dictionary<string, Dictionary<string, List<string>>>()
    {
        //Bus QuestionOptions
        { "mobility/bus", new Dictionary<string, List<string>>() {
            { "default", ["Diesel", "Electric"]},
            { "Austria", ["Diesel", "Electric"]},
            { "Belgium", ["Diesel", "Electric"]},
            { "Bulgaria", ["Diesel", "Electric"]},
            { "Croatia", ["Diesel", "Electric"]},
            { "Cyprus", ["Diesel", "Electric"]},
            { "Czechia", ["Diesel", "Electric"]},
            { "Denmark", ["Diesel", "Electric"]},
            { "Estonia", ["Diesel", "Electric"]},
            { "Finland", ["Diesel", "Electric"]},
            { "France", ["Diesel", "Electric"]},
            { "Germany", ["Diesel", "Electric"]},
            { "Greece", ["Diesel", "Electric"]},
            { "Hungary", ["Diesel", "Electric"]},
            { "Iceland", ["Diesel", "Electric"]},
            { "Ireland", ["Diesel", "Electric"]},
            { "Italy", ["Diesel", "Electric"]},
            { "Latvia", ["Diesel", "Electric"]},
            { "Lithuania", ["Diesel", "Electric"]},
            { "Luxemburg", ["Diesel", "Electric"]},
            { "Malta", ["Diesel", "Electric"]},
            { "Netherlands", ["Diesel", "Electric"]},
            { "Norway", ["Diesel", "Electric"]},
            { "Poland", ["Diesel", "Electric"]},
            { "Portugal", ["Diesel", "Electric"]},
            { "Romania", ["Diesel", "Electric"]},
            { "Serbia", ["Diesel", "Electric"]},
            { "Slovakia", ["Diesel", "Electric"]},
            { "Slovenia", ["Diesel", "Electric"]},
            { "Spain", ["Diesel", "Electric"]},
            { "Sweden", ["Diesel", "Electric"]},
            { "Switserland", ["Diesel", "Electric"]},
            { "UK", ["Diesel", "Electric"]},
            { "Australia", ["Diesel", "Electric"]},
            { "New-Zealand", ["Diesel", "Electric"]},
            { "Canada", ["Diesel", "Electric"]},
            { "US", ["Diesel", "Electric"]},
        }},

         //Light rail QuestionOptions
        { "mobility/light-rail", new Dictionary<string, List<string>>() {
            { "default", ["Tram", "Metro"]},
            { "Austria", ["Tram", "Metro"]},
            { "Belgium", ["Tram", "Metro"]},
            { "Bulgaria", ["Tram", "Metro"]},
            { "Croatia", ["Tram", "Metro"]},
            { "Cyprus", ["Tram", "Metro"]},
            { "Czechia", ["Tram", "Metro"]},
            { "Denmark", ["Tram", "Metro"]},
            { "Estonia", ["Tram", "Metro", "Tallinna Linnatransport"]},
            { "Finland", ["Tram", "Metro"]},
            { "France", ["Tram", "Metro"]},
            { "Germany", ["Tram", "Metro"]},
            { "Greece", ["Tram", "Metro"]},
            { "Hungary", ["Tram", "Metro"]},
            { "Iceland", ["Tram", "Metro"]},
            { "Ireland", ["Tram", "Metro"]},
            { "Italy", ["Tram", "Metro"]},
            { "Latvia", ["Tram", "Metro"]},
            { "Lithuania", ["Tram", "Metro"]},
            { "Luxemburg", ["Tram", "Metro"]},
            { "Malta", ["Tram", "Metro"]},
            { "Netherlands", ["Tram", "Metro"]},
            { "Norway", ["Tram", "Metro"]},
            { "Poland", ["Tram", "Metro"]},
            { "Portugal", ["Tram", "Metro"]},
            { "Romania", ["Tram", "Metro"]},
            { "Serbia", ["Tram", "Metro"]},
            { "Slovakia", ["Tram", "Metro"]},
            { "Slovenia", ["Tram", "Metro"]},
            { "Spain", ["Tram", "Metro"]},
            { "Sweden", ["Tram", "Metro"]},
            { "Switserland", ["Tram", "Metro"]},
            { "UK", ["Tram", "Metro"]},
            { "Australia", ["Tram", "Metro"]},
            { "Australia-Victoria", ["Tram", "Metro", "Melbourne-light-rail"]},
            { "Australia-NSW", ["Tram", "Metro", "Sydney-metro"]},
            { "Australia-ACT", ["Tram", "Metro", "Canberra-metro"]},
            { "New-Zealand", ["Tram", "Metro"]},
            { "Canada", ["Tram", "Metro"]},
            { "US", ["Tram", "Metro"]},
        }},

        //Rail QuestionOptions
        { "mobility/rail", new Dictionary<string, List<string>>() {
            { "default", ["Average", "Diesel", "Electric"]},
            { "Austria", ["Average", "Diesel", "Electric"]},
            { "Belgium", ["Average", "Diesel", "Electric"]},
            { "Bulgaria", ["Average", "Diesel", "Electric"]},
            { "Croatia", ["Average", "Diesel", "Electric"]},
            { "Cyprus", []},
            { "Czechia", ["Average", "Diesel", "Electric"]},
            { "Denmark", ["Average", "S-Tog", "Diesel", "Electric"]},
            { "Estonia", ["Average", "Diesel", "Electric"]},
            { "Finland", ["Average", "Diesel", "Electric"]},
            { "France", ["Average", "TER", "Electric (Translinien, RER, IC, TGV)"]},
            { "Germany", ["Average", "Diesel", "Electric", "ICE & IC"]},
            { "Greece", ["Average", "Diesel", "Electric"]},
            { "Hungary", ["Average", "Diesel", "Electric"]},
            { "Iceland", ["Average", "Diesel", "Electric"]},
            { "Ireland", ["Average", "Diesel", "Electric", "DART"]},
            { "Italy", ["Average", "Diesel", "Electric"]},
            { "Latvia", ["Average", "Diesel", "Electric"]},
            { "Lithuania", ["Average", "Diesel", "Electric"]},
            { "Luxemburg", ["Average"]},
            { "Malta", []},
            { "Netherlands", ["Average"]},
            { "Norway", ["Average", "Diesel", "Electric"]},
            { "Poland", ["Average", "Diesel", "Electric"]},
            { "Portugal", ["Average", "Diesel", "Electric"]},
            { "Romania", ["Average", "Diesel", "Electric"]},
            { "Serbia", ["Average", "Diesel", "Electric"]},
            { "Slovakia", ["Average", "Diesel", "Electric"]},
            { "Slovenia", ["Average", "Diesel", "Electric"]},
            { "Spain", ["Average", "Diesel", "Electric"]},
            { "Sweden", ["Average", "Diesel", "Electric"]},
            { "Switserland", ["Average"]},
            { "UK", ["Average", "Diesel", "Electric"]},
            { "Australia", ["Average", "Diesel", "Electric"]},
            { "New-Zealand", ["Average", "Auckland Commuter Rail"]},
            { "Canada", ["Average", "Diesel", "Electric"]},
            { "US", ["Average", "Diesel", "Electric"]},
        }},
    };
}