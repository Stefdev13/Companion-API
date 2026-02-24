public class V1SurveyModelGenerator : ISurveyModelGenerator
{
    public SurveyModel GenerateSurveyModel(string country, string? region)
    {
        SurveyModel result = new SurveyModel()
        {
            Version = "1",
            SurveyCategories = [],
            ReusableQuestionOptions = [],
        };

        CreateAndAddMobilitySurveyCategory(result);
        CreateAndAddTravelSurveyCategory(result);
        CreateAndAddHomeSurveyCategory(result);
        CreateAndAddFoodSurveyCategory(result);
        CreateAndAddPurchasingtySurveyCategory(result);

        return result;
    }

    //ANCHOR[epic=Mobility]
    public void CreateAndAddMobilitySurveyCategory(SurveyModel surveyModel)
    {
        SurveyCategory mobility = new SurveyCategory()
        {
            CategoryName = "Mobility",
            Description = "Cars, bikes, taxi's, public transit and frequent journeys. Not including travel.",
            Questions = []
        };

        mobility.Questions.Add(CreateCarQuestion());
        mobility.Questions.Add(CreateMotorbikeQuestion());
        mobility.Questions.Add(CreateBikesQuestion());
        mobility.Questions.Add(CreateTaxiQuestion());
        mobility.Questions.Add(CreateBusQuestion());
        mobility.Questions.Add(CreateTramSubwayQuestion());
        mobility.Questions.Add(CreateRailQuestion());
        mobility.Questions.Add(CreateRailQuestion());

        //Add mobility SurveyCategory to surveyModel
        surveyModel.SurveyCategories.Add(mobility);
    }

    public Question CreateCarQuestion()
    {
        Question carQuestion = new Question()
        {
            QuestionName = "What car(s) do you drive?",
            Description = "Please add all the cars that you use to get to places.",
            Tips = ["You can use the manufacturers emissions figures or use your own fuel consumption numbers"],
            QuestionOptions = [],
            AllowCustomOptions = true,
            NewCustomOptionPrompt = "Add a car",
            AllowReusableQuestionOptions = false,
            ReusableQuestionOptionsTags = ["mobility", "cars"],
        };

        StandardQuestionOption carQuestionOptionTemplate = new StandardQuestionOption()
        {
            Name = "",
            IsSelected = true,
            DisplaySubQuestions = [],
            Tags = ["mobility", "cars"],
            SubQuestions = [],
            UsedInOtherSurveyQuestions = true,
            CloneOfOtherQuestionOption = false,
            ShouldInfluenceOriginalQuestionOption = false,
        };
        carQuestion.OptionTemplate = carQuestionOptionTemplate;

        SubQuestion carUsage = new SubQuestion()
        {
            Question = "How much do you drive per year?",
            Description = "In kilometers or miles",
            QuestionType = QuestionType.intInput,
            UnitOptions = ["km", "mi"],
            DefaultMetricUnit = "km",
            DefaultImperialUnit = "mi",
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_annual_usage,
        };
        carQuestionOptionTemplate.DisplaySubQuestions.Add(carUsage);
        carQuestionOptionTemplate.SubQuestions.Add(carUsage);

        SubQuestion firstSecondHand = new SubQuestion()
        {
            Question = "Is the car first or second hand?",
            Description = "",
            QuestionType = QuestionType.toggle,
            AnswerOptions = ["First hand", "Second hand"],
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_first_or_second_hand,
        };
        carQuestionOptionTemplate.SubQuestions.Add(firstSecondHand);

        DisplayRule firstHandCarDisplayRule = new DisplayRule()
        {
            SubQuestion = firstSecondHand,
            ValidValues = ["First hand"]
        };

        SubQuestion carOwnership = new SubQuestion()
        {
            Question = "Years of ownership",
            Description = "How many years do you expect to own this car?",
            QuestionType = QuestionType.intInput,
            UnitOptions = ["years"],
            DefaultMetricUnit = "years",
            DefaultImperialUnit = "years",
            Answer = "",
            DisplayRules = [firstHandCarDisplayRule],
            SubQuestionKey = V1SubQuestionKeys.mobility_years_of_ownership,
        };
        carQuestionOptionTemplate.SubQuestions.Add(carOwnership);

        SubQuestion carType = new SubQuestion()
        {
            Question = "What kind of car is it?",
            Description = "Petrol, diesel, hybrid, plug-in hybrid or electric",
            QuestionType = QuestionType.select,
            AnswerOptions = ["Petrol", "Diesel", "Hybrid", "Plug-in hybrid", "Electric"],
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_vehicle_fuel,
        };
        carQuestionOptionTemplate.DisplaySubQuestions.Add(carType);
        carQuestionOptionTemplate.SubQuestions.Add(carType);

        SubQuestion carSize = new SubQuestion()
        {
            Question = "How big is the car?",
            Description = "Small, medium or large",
            QuestionType = QuestionType.select,
            AnswerOptions = ["Small", "Medium", "Large"],
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_vehicle_size,
        };
        carQuestionOptionTemplate.SubQuestions.Add(carSize);

        SubQuestion usageOrManufacturerRatings = new SubQuestion()
        {
            Question = "Personal fuel consumption or manufacturers figures?",
            Description = "Would you like to use your own fuel comsumption figures or use the manufacturers figures?",
            QuestionType = QuestionType.toggle,
            AnswerOptions = ["Personal", "Manufacturer"],
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_consumption_or_manufacturer,
        };
        carQuestionOptionTemplate.SubQuestions.Add(usageOrManufacturerRatings);

        DisplayRule manufacturersRatingsDisplayRule = new DisplayRule()
        {
            SubQuestion = usageOrManufacturerRatings,
            ValidValues = ["Manufacturer"]
        };

        SubQuestion carEmissionsFigure = new SubQuestion()
        {
            Question = "Emissions figures",
            Description = "What is your cars emissions rating? In co2e/km of co2e/mi",
            QuestionType = QuestionType.doubleInputOrAvg,
            UnitOptions = ["co2e/km", "co2e/mi"],
            DefaultMetricUnit = "co2e/km",
            DefaultImperialUnit = "co2e/mi",
            Answer = "",
            DisplayRules = [manufacturersRatingsDisplayRule],
            SubQuestionKey = V1SubQuestionKeys.mobility_vehicle_co2e_rating,
        };
        carQuestionOptionTemplate.DisplaySubQuestions.Add(carEmissionsFigure);
        carQuestionOptionTemplate.SubQuestions.Add(carEmissionsFigure);

        DisplayRule personalUsageDisplayRule = new DisplayRule()
        {
            SubQuestion = usageOrManufacturerRatings,
            ValidValues = ["Personal"]
        };

        DisplayRule iceTypeDisplayRule = new DisplayRule()
        {
            SubQuestion = carType,
            ValidValues = ["Petrol", "Diesel", "Hybrid", "Plug-in hybrid"]
        };

        DisplayRule electricTypeDisplayRule = new DisplayRule()
        {
            SubQuestion = carType,
            ValidValues = ["Electric"]
        };

        SubQuestion carFuelConsumption = new SubQuestion()
        {
            Question = "Fuel consumption",
            Description = "How much fuel does your car use? In l/100km or mi/gallon.",
            QuestionType = QuestionType.doubleInput,
            UnitOptions = ["l/100km", "mi/gallon"],
            DefaultMetricUnit = "l/100km",
            DefaultImperialUnit = "mi/gallon",
            Answer = "",
            DisplayRules = [personalUsageDisplayRule, iceTypeDisplayRule],
            SubQuestionKey = V1SubQuestionKeys.mobility_vehicle_fuel_consumption,
        };
        carQuestionOptionTemplate.DisplaySubQuestions.Add(carFuelConsumption);
        carQuestionOptionTemplate.SubQuestions.Add(carFuelConsumption);

        SubQuestion carKWhConsumption = new SubQuestion()
        {
            Question = "Energy consumption",
            Description = "How efficient is your car? In kWh/100km or mi/kWh.",
            QuestionType = QuestionType.doubleInput,
            UnitOptions = ["kWh/100km", "mi/kWh"],
            DefaultMetricUnit = "kWh/100km",
            DefaultImperialUnit = "mi/kWh",
            Answer = "",
            DisplayRules = [personalUsageDisplayRule, electricTypeDisplayRule],
            SubQuestionKey = V1SubQuestionKeys.mobility_vehicle_kwh_consumption,
        };
        carQuestionOptionTemplate.DisplaySubQuestions.Add(carKWhConsumption);
        carQuestionOptionTemplate.SubQuestions.Add(carKWhConsumption);

        DisplayRule electricOrPHEVDisplayRule = new DisplayRule()
        {
            SubQuestion = carType,
            ValidValues = ["Plug-in hybrid", "Electric"]
        };

        SubQuestion carBatterySize = new SubQuestion()
        {
            Question = "Battery size",
            Description = "How big is the battery in your car? In kWh",
            QuestionType = QuestionType.doubleInputOrAvg,
            UnitOptions = ["kWh"],
            DefaultMetricUnit = "kWh",
            DefaultImperialUnit = "kWh",
            Answer = "",
            DisplayRules = [electricOrPHEVDisplayRule],
            SubQuestionKey = V1SubQuestionKeys.mobility_vehicle_battery_size,
        };
        carQuestionOptionTemplate.SubQuestions.Add(carBatterySize);

        SubQuestion carChargeSource = new SubQuestion()
        {
            Question = "Charging source",
            Description = "What percentage of the electricity used by your car came from renewable sources?",
            QuestionType = QuestionType.percentage,
            Answer = "",
            DisplayRules = [electricOrPHEVDisplayRule],
            SubQuestionKey = V1SubQuestionKeys.mobility_vehicle_charging_source,
        };
        carQuestionOptionTemplate.SubQuestions.Add(carChargeSource);

        return carQuestion;
    }

    public Question CreateMotorbikeQuestion()
    {
        Question motorbikeQuestion = new Question()
        {
            QuestionName = "What motorbike(s) do you ride?",
            Description = "Please add all the motorbikes that you use to get to places.",
            Tips = ["You can use the manufacturers emissions figures or use your own fuel consumption numbers"],
            QuestionOptions = [],
            AllowCustomOptions = true,
            NewCustomOptionPrompt = "Add a motorbike",
            AllowReusableQuestionOptions = false,
            ReusableQuestionOptionsTags = ["mobility", "motorbikes"],
        };

        StandardQuestionOption motorbikeQuestionOptionTemplate = new StandardQuestionOption()
        {
            Name = "",
            IsSelected = true,
            DisplaySubQuestions = [],
            Tags = ["mobility", "motorbikes"],
            SubQuestions = [],
            UsedInOtherSurveyQuestions = true,
            CloneOfOtherQuestionOption = false,
            ShouldInfluenceOriginalQuestionOption = false,
        };
        motorbikeQuestion.OptionTemplate = motorbikeQuestionOptionTemplate;

        SubQuestion motorbikeUsage = new SubQuestion()
        {
            Question = "How much do you ride per year?",
            Description = "In kilometers or miles",
            QuestionType = QuestionType.intInput,
            UnitOptions = ["km", "mi"],
            DefaultMetricUnit = "km",
            DefaultImperialUnit = "mi",
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_annual_usage,
        };
        motorbikeQuestionOptionTemplate.DisplaySubQuestions.Add(motorbikeUsage);
        motorbikeQuestionOptionTemplate.SubQuestions.Add(motorbikeUsage);

        SubQuestion firstSecondHand = new SubQuestion()
        {
            Question = "Is the motorbike first or second hand?",
            Description = "",
            QuestionType = QuestionType.toggle,
            AnswerOptions = ["First hand", "Second hand"],
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_first_or_second_hand,
        };
        motorbikeQuestionOptionTemplate.SubQuestions.Add(firstSecondHand);

        DisplayRule firstHandMotorbikeDisplayRule = new DisplayRule()
        {
            SubQuestion = firstSecondHand,
            ValidValues = ["First hand"]
        };

        SubQuestion motorbikeOwnership = new SubQuestion()
        {
            Question = "Years of ownership",
            Description = "How many years do you expect to own this motorbike?",
            QuestionType = QuestionType.intInput,
            UnitOptions = ["years"],
            DefaultMetricUnit = "years",
            DefaultImperialUnit = "years",
            Answer = "",
            DisplayRules = [firstHandMotorbikeDisplayRule],
            SubQuestionKey = V1SubQuestionKeys.mobility_years_of_ownership,
        };
        motorbikeQuestionOptionTemplate.SubQuestions.Add(motorbikeOwnership);

        SubQuestion motorbikeType = new SubQuestion()
        {
            Question = "What kind of motorbike is it?",
            Description = "Petrol or electric",
            QuestionType = QuestionType.select,
            AnswerOptions = ["Petrol", "Electric"],
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_vehicle_fuel,
        };
        motorbikeQuestionOptionTemplate.DisplaySubQuestions.Add(motorbikeType);
        motorbikeQuestionOptionTemplate.SubQuestions.Add(motorbikeType);

        SubQuestion motorbikeSize = new SubQuestion()
        {
            Question = "How big is the motobike?",
            Description = "Small, medium or large",
            QuestionType = QuestionType.select,
            AnswerOptions = ["Small", "Medium", "Large"],
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_vehicle_size,
        };
        motorbikeQuestionOptionTemplate.SubQuestions.Add(motorbikeSize);

        SubQuestion usageOrManufacturerRatings = new SubQuestion()
        {
            Question = "Personal fuel consumption or manufacturers figures?",
            Description = "Would you like to use your own fuel comsumption figures or use the manufacturers figures?",
            QuestionType = QuestionType.toggle,
            AnswerOptions = ["Personal", "Manufacturer"],
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_consumption_or_manufacturer,
        };
        motorbikeQuestionOptionTemplate.SubQuestions.Add(usageOrManufacturerRatings);

        DisplayRule manufacturersRatingsDisplayRule = new DisplayRule()
        {
            SubQuestion = usageOrManufacturerRatings,
            ValidValues = ["Manufacturer"]
        };

        SubQuestion motobikeEmissionsFigure = new SubQuestion()
        {
            Question = "Emissions figures",
            Description = "What is your motobike's emissions rating? In co2e/km of co2e/mi",
            QuestionType = QuestionType.doubleInputOrAvg,
            UnitOptions = ["co2e/km", "co2e/mi"],
            DefaultMetricUnit = "co2e/km",
            DefaultImperialUnit = "co2e/mi",
            Answer = "",
            DisplayRules = [manufacturersRatingsDisplayRule],
            SubQuestionKey = V1SubQuestionKeys.mobility_vehicle_co2e_rating,
        };
        motorbikeQuestionOptionTemplate.DisplaySubQuestions.Add(motobikeEmissionsFigure);
        motorbikeQuestionOptionTemplate.SubQuestions.Add(motobikeEmissionsFigure);

        DisplayRule personalUsageDisplayRule = new DisplayRule()
        {
            SubQuestion = usageOrManufacturerRatings,
            ValidValues = ["Personal"]
        };

        DisplayRule ICETypeDisplayRule = new DisplayRule()
        {
            SubQuestion = motorbikeType,
            ValidValues = ["Petrol"]
        };

        DisplayRule electricTypeDisplayRule = new DisplayRule()
        {
            SubQuestion = motorbikeType,
            ValidValues = ["Electric"]
        };

        SubQuestion motorbikeFuelConsumption = new SubQuestion()
        {
            Question = "Fuel consumption",
            Description = "How much fuel does your motorbike use? In l/100km or mi/gallon.",
            QuestionType = QuestionType.doubleInput,
            UnitOptions = ["l/100km", "mi/gallon"],
            DefaultMetricUnit = "l/100km",
            DefaultImperialUnit = "mi/gallon",
            Answer = "",
            DisplayRules = [personalUsageDisplayRule, ICETypeDisplayRule],
            SubQuestionKey = V1SubQuestionKeys.mobility_vehicle_fuel_consumption,
        };
        motorbikeQuestionOptionTemplate.DisplaySubQuestions.Add(motorbikeFuelConsumption);
        motorbikeQuestionOptionTemplate.SubQuestions.Add(motorbikeFuelConsumption);

        SubQuestion motorbikeKWhConsumption = new SubQuestion()
        {
            Question = "Energy consumption",
            Description = "How efficient is your motorbike? In kWh/100km or mi/kWh.",
            QuestionType = QuestionType.doubleInput,
            UnitOptions = ["kWh/100km", "mi/kWh"],
            DefaultMetricUnit = "kWh/100km",
            DefaultImperialUnit = "mi/kWh",
            Answer = "",
            DisplayRules = [personalUsageDisplayRule, electricTypeDisplayRule],
            SubQuestionKey = V1SubQuestionKeys.mobility_vehicle_kwh_consumption,
        };
        motorbikeQuestionOptionTemplate.DisplaySubQuestions.Add(motorbikeKWhConsumption);
        motorbikeQuestionOptionTemplate.SubQuestions.Add(motorbikeKWhConsumption);

        SubQuestion motorbikeBatterySize = new SubQuestion()
        {
            Question = "Battery size",
            Description = "How big is the battery in your motorbike? In kWh",
            QuestionType = QuestionType.doubleInputOrAvg,
            UnitOptions = ["kWh"],
            DefaultMetricUnit = "kWh",
            DefaultImperialUnit = "kWh",
            Answer = "",
            DisplayRules = [electricTypeDisplayRule],
            SubQuestionKey = V1SubQuestionKeys.mobility_vehicle_battery_size,
        };
        motorbikeQuestionOptionTemplate.SubQuestions.Add(motorbikeBatterySize);

        SubQuestion motorbikeChargeSource = new SubQuestion()
        {
            Question = "Charging source",
            Description = "What percentage of the electricity used by your motorbike came from renewable sources?",
            QuestionType = QuestionType.percentage,
            Answer = "",
            DisplayRules = [electricTypeDisplayRule],
            SubQuestionKey = V1SubQuestionKeys.mobility_vehicle_charging_source,
        };
        motorbikeQuestionOptionTemplate.SubQuestions.Add(motorbikeChargeSource);

        return motorbikeQuestion;
    }

    public Question CreateBikesQuestion()
    {
        Question bikeQuestion = new Question()
        {
            QuestionName = "What bike(s) and e-bike(s) do you ride?",
            Description = "Please add all the (e-)bikes that you use to get to places.",
            Tips = [],
            QuestionOptions = [],
            AllowCustomOptions = true,
            NewCustomOptionPrompt = "Add a (e-)bike",
            AllowReusableQuestionOptions = false,
            ReusableQuestionOptionsTags = ["mobility", "bikes"],
        };

        StandardQuestionOption bikeQuestionOptionTemplate = new StandardQuestionOption()
        {
            Name = "",
            IsSelected = true,
            DisplaySubQuestions = [],
            Tags = ["mobility", "bikes"],
            SubQuestions = [],
            UsedInOtherSurveyQuestions = true,
            CloneOfOtherQuestionOption = false,
            ShouldInfluenceOriginalQuestionOption = false,
        };
        bikeQuestion.OptionTemplate = bikeQuestionOptionTemplate;

        SubQuestion bikeUsage = new SubQuestion()
        {
            Question = "How much do you cycle per year?",
            Description = "In kilometers or miles",
            QuestionType = QuestionType.intInput,
            UnitOptions = ["km", "mi"],
            DefaultMetricUnit = "km",
            DefaultImperialUnit = "mi",
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_annual_usage,
        };
        bikeQuestionOptionTemplate.DisplaySubQuestions.Add(bikeUsage);
        bikeQuestionOptionTemplate.SubQuestions.Add(bikeUsage);

        SubQuestion firstSecondHand = new SubQuestion()
        {
            Question = "Is the (e-)bike first or second hand?",
            Description = "",
            QuestionType = QuestionType.toggle,
            AnswerOptions = ["First hand", "Second hand"],
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_first_or_second_hand,
        };
        bikeQuestionOptionTemplate.SubQuestions.Add(firstSecondHand);

        DisplayRule firstHandBikeDisplayRule = new DisplayRule()
        {
            SubQuestion = firstSecondHand,
            ValidValues = ["First hand"]
        };

        SubQuestion bikeOwnership = new SubQuestion()
        {
            Question = "Years of ownership",
            Description = "How many years do you expect to own this (e-)bike?",
            QuestionType = QuestionType.intInput,
            UnitOptions = ["years"],
            DefaultMetricUnit = "years",
            DefaultImperialUnit = "years",
            Answer = "",
            DisplayRules = [firstHandBikeDisplayRule],
            SubQuestionKey = V1SubQuestionKeys.mobility_years_of_ownership,
        };
        bikeQuestionOptionTemplate.SubQuestions.Add(bikeOwnership);

        SubQuestion bikeType = new SubQuestion()
        {
            Question = "What kind of bike is it?",
            Description = "E-bike or regular bike",
            QuestionType = QuestionType.select,
            AnswerOptions = ["Regular bike", "Electric"],
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_vehicle_fuel,
        };
        bikeQuestionOptionTemplate.DisplaySubQuestions.Add(bikeType);
        bikeQuestionOptionTemplate.SubQuestions.Add(bikeType);

        DisplayRule electricTypeDisplayRule = new DisplayRule()
        {
            SubQuestion = bikeType,
            ValidValues = ["Electric"]
        };

        SubQuestion bikeBatterySize = new SubQuestion()
        {
            Question = "Battery size",
            Description = "How big is the battery in your e-bike? In Wh",
            QuestionType = QuestionType.doubleInputOrAvg,
            UnitOptions = ["Wh"],
            DefaultMetricUnit = "Wh",
            DefaultImperialUnit = "Wh",
            Answer = "",
            DisplayRules = [electricTypeDisplayRule],
            SubQuestionKey = V1SubQuestionKeys.mobility_vehicle_battery_size,
        };
        bikeQuestionOptionTemplate.SubQuestions.Add(bikeBatterySize);

        SubQuestion bikeChargeSource = new SubQuestion()
        {
            Question = "Charging source",
            Description = "What percentage of the electricity used by your e-bike came from renewable sources?",
            QuestionType = QuestionType.percentage,
            Answer = "",
            DisplayRules = [electricTypeDisplayRule],
            SubQuestionKey = V1SubQuestionKeys.mobility_vehicle_charging_source,
        };
        bikeQuestionOptionTemplate.SubQuestions.Add(bikeChargeSource);

        return bikeQuestion;
    }

    public Question CreateTaxiQuestion()
    {
        Question taxiAndRideshareQuestion = new Question()
        {
            QuestionName = "How much do you ride in taxi's and rideshare's?",
            Description = "Enter how many kilometers or miles you ride in electric or regular taxi's/rideshare's",
            Tips = [],
            QuestionOptions = [],
            AllowCustomOptions = false,
            AllowReusableQuestionOptions = false,
            ReusableQuestionOptionsTags = ["mobility", "taxi's"],
        };

        StandardQuestionOption regularTaxiQuestionOption = new StandardQuestionOption()
        {
            Name = "Regular Taxi's and rideshare's",
            IsSelected = false,
            DisplaySubQuestions = [],
            Tags = ["mobility", "taxi's"],
            SubQuestions = [],
            UsedInOtherSurveyQuestions = true,
            CloneOfOtherQuestionOption = false,
            ShouldInfluenceOriginalQuestionOption = false,
        };

        SubQuestion regularTaxiUsageSubQuestion = new SubQuestion()
        {
            Question = "How much do you ride in taxi's or rideshare's per year?",
            Description = "In kilometers or miles",
            QuestionType = QuestionType.intInput,
            UnitOptions = ["km", "mi"],
            DefaultMetricUnit = "km",
            DefaultImperialUnit = "mi",
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_annual_usage,
        };
        regularTaxiQuestionOption.SubQuestions.Add(regularTaxiUsageSubQuestion);
        taxiAndRideshareQuestion.QuestionOptions.Add(regularTaxiQuestionOption);

        StandardQuestionOption electricTaxiQuestionOption = new StandardQuestionOption()
        {
            Name = "Electric Taxi's and rideshare's",
            IsSelected = false,
            DisplaySubQuestions = [],
            Tags = ["mobility", "taxi's"],
            SubQuestions = [],
            UsedInOtherSurveyQuestions = true,
            CloneOfOtherQuestionOption = false,
            ShouldInfluenceOriginalQuestionOption = false,
        };

        SubQuestion electricTaxiUsageSubQuestion = new SubQuestion()
        {
            Question = "How much do you ride in taxi's or rideshare's per year?",
            Description = "In kilometers or miles",
            QuestionType = QuestionType.intInput,
            UnitOptions = ["km", "mi"],
            DefaultMetricUnit = "km",
            DefaultImperialUnit = "mi",
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_annual_usage,
        };
        electricTaxiQuestionOption.SubQuestions.Add(electricTaxiUsageSubQuestion);
        taxiAndRideshareQuestion.QuestionOptions.Add(electricTaxiQuestionOption);

        return taxiAndRideshareQuestion;
    }

    public Question CreateBusQuestion()
    {

    }

    public Question CreateTramSubwayQuestion()
    {

    }

    public Question CreateRailQuestion()
    {

    }

    public Question CreateFJQuestion()
    {
        //TODO: Figure out how to do this one
    }

    //ANCHOR[epic=Travel]
    public void CreateAndAddTravelSurveyCategory(SurveyModel surveyModel)
    {
        //TODO: Create SurveyCategory
        //TODO: Create Questions
        //TODO: Create QuestionOptions and QuestionOptionTemplates
        //TODO: Create SubQuestions for QuestionOptions/QuestionOptionTemplates
        //TODO: Create DisplayRules for SubQuestions

        //TODO: Add Travel SurveyCategory to surveyModel
    }

    //ANCHOR[epic=Home]
    public void CreateAndAddHomeSurveyCategory(SurveyModel surveyModel)
    {
        //TODO: Create SurveyCategory
        //TODO: Create Questions
        //TODO: Create QuestionOptions
        //TODO: Create SubQuestions for QuestionOptions
        //TODO: Create DisplayRules for SubQuestions

        //TODO: Add Home SurveyCategory to surveyModel
    }

    //ANCHOR[epic=Food]
    public void CreateAndAddFoodSurveyCategory(SurveyModel surveyModel)
    {
        //TODO: Create SurveyCategory
        //TODO: Create Questions
        //TODO: Create QuestionOptions
        //TODO: Create SubQuestions for QuestionOptions
        //TODO: Create DisplayRules for SubQuestions

        //TODO: Add Food SurveyCategory to surveyModel
    }

    //ANCHOR[epic=Purchasing habits] - purchasing
    public void CreateAndAddPurchasingtySurveyCategory(SurveyModel surveyModel)
    {
        //TODO: Create SurveyCategory
        //TODO: Create Questions
        //TODO: Create QuestionOptions
        //TODO: Create SubQuestions for QuestionOptions
        //TODO: Create DisplayRules for SubQuestions

        //TODO: Add Purchasing SurveyCategory to surveyModel
    }
}