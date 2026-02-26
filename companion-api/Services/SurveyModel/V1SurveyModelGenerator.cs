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

    //SECTION[epic=Mobility] - Mobility
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
        Question busQuestion = new Question()
        {
            QuestionName = "How much do you ride on the bus per year?",
            Description = "In kilometers or miles.",
            Tips = [],
            QuestionOptions = [],
            AllowCustomOptions = false,
            AllowReusableQuestionOptions = false,
            ReusableQuestionOptionsTags = ["mobility", "bus"],
            DynamicQuestionOptionParams = ["mobility", "bus"],
        };

        return busQuestion;
    }

    public Question CreateTramSubwayQuestion()
    {
        Question lightRailQuestion = new Question()
        {
            QuestionName = "How much do you ride on trams and subways per year?",
            Description = "In kilometers or miles.",
            Tips = [],
            QuestionOptions = [],
            AllowCustomOptions = false,
            AllowReusableQuestionOptions = false,
            ReusableQuestionOptionsTags = ["mobility", "light-rail"],
            DynamicQuestionOptionParams = ["mobility", "light-rail"],
        };

        return lightRailQuestion;
    }

    public Question CreateRailQuestion()
    {
        Question railQuestion = new Question()
        {
            QuestionName = "How much do you ride on the trains per year?",
            Description = "In kilometers or miles.",
            Tips = [],
            QuestionOptions = [],
            AllowCustomOptions = false,
            AllowReusableQuestionOptions = false,
            ReusableQuestionOptionsTags = ["mobility", "rail"],
            DynamicQuestionOptionParams = ["mobility", "rail"],
        };

        return railQuestion;
    }

    public Question CreateFJQuestion()
    {
        Question frequentJourneyQuestion = new Question()
        {
            QuestionName = "Do you have any journeys you frequently do?",
            Description = "Commuting to work for example. Adding frequent jouneys can make it much easier to implement and follow reductions",
            Tips = [],
            QuestionOptions = [],
            AllowCustomOptions = true,
            NewCustomOptionPrompt = "Add a frequent journey",
            AllowReusableQuestionOptions = false,
            ReusableQuestionOptionsTags = ["mobility", "frequent-journey"],
            DynamicQuestionOptionParams = ["mobility", "frequent-journey"],
        };

        SubQuestion roundTripSubQuestion = new SubQuestion()
        {
            Question = "Is this a round-trip?",
            Description = "Selecting round-trip will add the same legs in reverse order at the end of the journey",
            QuestionType = QuestionType.toggle,
            AnswerOptions = ["One-way", "Round-trip"],
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_fj_round_trip,
        };

        SubQuestion frequencySubQuestion = new SubQuestion()
        {
            Question = "How often do you make this journey?",
            Description = "",
            QuestionType = QuestionType.frequency,
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_fj_frequency,
        };

        SubQuestion startDateSubQuestion = new SubQuestion()
        {
            Question = "Starts on",
            Description = "",
            QuestionType = QuestionType.date,
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_fj_start_date,
        };

        SubQuestion endDateSubQuestion = new SubQuestion()
        {
            Question = "Ends on",
            Description = "",
            QuestionType = QuestionType.date,
            Answer = "",
            SubQuestionKey = V1SubQuestionKeys.mobility_fj_end_date,
        };

        FJQuestionOption frequentJourneyQuestionOptionTemplate = new FJQuestionOption()
        {
            Name = "",
            IsSelected = true,
            DisplaySubQuestions = [],
            Tags = ["mobility", "frequent-journey"],
            RoundTripSubQuestion = roundTripSubQuestion,
            FrequencySubQuestion = frequencySubQuestion,
            StartDateSubQuestion = startDateSubQuestion,
            EndDateSubQuestion = endDateSubQuestion,
            Legs = [],
        };
        frequentJourneyQuestion.OptionTemplate = frequentJourneyQuestionOptionTemplate;

        return frequentJourneyQuestion;
    }
    //!SECTION[epic=Mobility] - Mobility

    //SECTION[epic=Travel] - Travel
    public void CreateAndAddTravelSurveyCategory(SurveyModel surveyModel)
    {
        SurveyCategory travel = new SurveyCategory()
        {
            CategoryName = "Travel",
            Description = "Going on vacation or other forms of long distance travel.",
            Questions = [],
        };

        travel.Questions.Add(CreateFlightsQuestion());
        travel.Questions.Add(CreateCarTravelQuestion());
        travel.Questions.Add(CreateTrainTravelQuestion());
        travel.Questions.Add(CreateBusTravelQuestion());
        travel.Questions.Add(CreateCruiseQuestion());
        travel.Questions.Add(CreateFerryQuestion());

        //Add mobility SurveyCategory to surveyModel
        surveyModel.SurveyCategories.Add(travel);
    }

    public Question CreateFlightsQuestion()
    {
        Question question = new Question()
        {
            QuestionName = "How much do you fly per year?",
            Description = "On short haul flight (up to 4 hours) and long haul flights (4+ hours). You can answer in distance flown or hours flown.",
            Tips = [],
            AllowCustomOptions = false,
            AllowReusableQuestionOptions = false,
            QuestionOptions = [],
            ReusableQuestionOptionsTags = ["travel", "flights"],
        };

        question.QuestionOptions.Add(createShortHaulQuestionOption());
        question.QuestionOptions.Add(createLongHaulQuestionOption());

        return question;

        StandardQuestionOption createShortHaulQuestionOption()
        {
            StandardQuestionOption shortHaulFlights = new StandardQuestionOption()
            {
                Name = "Short haul flights",
                IsSelected = false,
                Tags = ["travel", "flights"],
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion hoursDistanceToggleSH = new SubQuestion()
            {
                Question = "Hours or distance?",
                Description = "",
                QuestionType = QuestionType.toggle,
                AnswerOptions = ["Hours", "Distance"],
                Answer = "Distance",
                SubQuestionKey = V1SubQuestionKeys.travel_hours_or_distance,
            };
            shortHaulFlights.SubQuestions.Add(hoursDistanceToggleSH);

            DisplayRule hoursFlownDisplayRuleSH = new DisplayRule()
            {
                SubQuestion = hoursDistanceToggleSH,
                ValidValues = ["Hours"]
            };

            DisplayRule distanceFlownDisplayRuleSH = new DisplayRule()
            {
                SubQuestion = hoursDistanceToggleSH,
                ValidValues = ["Distance"]
            };

            SubQuestion hoursInputSH = new SubQuestion()
            {
                Question = "How many hours do you fly per year?",
                Description = "",
                QuestionType = QuestionType.intInput,
                Answer = "",
                UnitOptions = ["hours"],
                SelectedUnit = "hours",
                DefaultMetricUnit = "hours",
                DefaultImperialUnit = "hours",
                DisplayRules = [hoursFlownDisplayRuleSH],
                SubQuestionKey = V1SubQuestionKeys.travel_hours_travelled,
            };
            shortHaulFlights.SubQuestions.Add(hoursInputSH);
            shortHaulFlights.DisplaySubQuestions.Add(hoursInputSH);

            SubQuestion distanceInputSH = new SubQuestion()
            {
                Question = "How much do you fly per year?",
                Description = "",
                QuestionType = QuestionType.intInput,
                Answer = "",
                UnitOptions = ["km", "mi"],
                DefaultMetricUnit = "km",
                DefaultImperialUnit = "mi",
                DisplayRules = [distanceFlownDisplayRuleSH],
                SubQuestionKey = V1SubQuestionKeys.travel_distance_travelled,
            };
            shortHaulFlights.SubQuestions.Add(distanceInputSH);
            shortHaulFlights.DisplaySubQuestions.Add(distanceInputSH);

            return shortHaulFlights;
        }

        StandardQuestionOption createLongHaulQuestionOption()
        {
            StandardQuestionOption longHaulFlights = new StandardQuestionOption()
            {
                Name = "Long haul flights",
                IsSelected = false,
                Tags = ["travel", "flights"],
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion hoursDistanceToggleLH = new SubQuestion()
            {
                Question = "Hours or distance?",
                Description = "",
                QuestionType = QuestionType.toggle,
                AnswerOptions = ["Hours", "Distance"],
                Answer = "Distance",
                SubQuestionKey = V1SubQuestionKeys.travel_hours_or_distance,
            };
            longHaulFlights.SubQuestions.Add(hoursDistanceToggleLH);

            DisplayRule hoursFlownDisplayRuleLH = new DisplayRule()
            {
                SubQuestion = hoursDistanceToggleLH,
                ValidValues = ["Hours"]
            };

            DisplayRule distanceFlownDisplayRuleLH = new DisplayRule()
            {
                SubQuestion = hoursDistanceToggleLH,
                ValidValues = ["Distance"]
            };

            SubQuestion hoursInputLH = new SubQuestion()
            {
                Question = "How many hours do you fly per year?",
                Description = "",
                QuestionType = QuestionType.intInput,
                Answer = "",
                UnitOptions = ["hours"],
                SelectedUnit = "hours",
                DefaultMetricUnit = "hours",
                DefaultImperialUnit = "hours",
                DisplayRules = [hoursFlownDisplayRuleLH],
                SubQuestionKey = V1SubQuestionKeys.travel_hours_travelled,
            };
            longHaulFlights.SubQuestions.Add(hoursInputLH);
            longHaulFlights.DisplaySubQuestions.Add(hoursInputLH);

            SubQuestion distanceInputLH = new SubQuestion()
            {
                Question = "How much do you fly per year?",
                Description = "",
                QuestionType = QuestionType.intInput,
                Answer = "",
                UnitOptions = ["km", "mi"],
                DefaultMetricUnit = "km",
                DefaultImperialUnit = "mi",
                DisplayRules = [distanceFlownDisplayRuleLH],
                SubQuestionKey = V1SubQuestionKeys.travel_distance_travelled,
            };
            longHaulFlights.SubQuestions.Add(distanceInputLH);
            longHaulFlights.DisplaySubQuestions.Add(distanceInputLH);

            return longHaulFlights;
        }
    }

    public Question CreateCarTravelQuestion()
    {
        Question question = new Question()
        {
            QuestionName = "How much do you travel by car?",
            Description = "Please enter the total distance you drive in a year for vacations or other long distance travel.",
            Tips = [],
            AllowCustomOptions = false,
            AllowReusableQuestionOptions = true,
            QuestionOptions = [],
            ReusableQuestionOptionsTags = ["mobility", "cars"],
        };

        StandardQuestionOption questionOptionTemplate = new StandardQuestionOption()
        {
            Name = "",
            IsSelected = true,
            SubQuestions = [],
            DisplaySubQuestions = [],
            ShouldInfluenceOriginalQuestionOption = false,
            CloneOfOtherQuestionOption = true,
            Tags = ["travel", "cars"]
        };

        SubQuestion travelMileageSubQuestion = new SubQuestion()
        {
            Question = "How much do you travel per year?",
            Description = "In kilometers or miles",
            QuestionType = QuestionType.intInput,
            Answer = "",
            UnitOptions = ["km", "mi"],
            DefaultMetricUnit = "km",
            DefaultImperialUnit = "mi",
            SubQuestionKey = V1SubQuestionKeys.travel_distance_travelled
        };
        questionOptionTemplate.SubQuestions.Add(travelMileageSubQuestion);
        questionOptionTemplate.DisplaySubQuestions.Add(travelMileageSubQuestion);

        question.OptionTemplate = questionOptionTemplate;

        return question;
    }

    public Question CreateTrainTravelQuestion()
    {
        Question question = new Question()
        {
            QuestionName = "How much do you travel by train per year?",
            Description = "On high speed rail and regular rail. You can answer in distance travelled or hours travelled.",
            Tips = [],
            AllowCustomOptions = false,
            AllowReusableQuestionOptions = false,
            QuestionOptions = [],
            ReusableQuestionOptionsTags = ["travel", "train"],
        };

        question.QuestionOptions.Add(createHighSpeedRailQuestionOption());
        question.QuestionOptions.Add(createRegularRailQuestionOption());

        return question;

        StandardQuestionOption createHighSpeedRailQuestionOption()
        {
            StandardQuestionOption highSpeedRail = new StandardQuestionOption()
            {
                Name = "High Speed Rail",
                IsSelected = false,
                Tags = ["travel", "train"],
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion hoursDistanceToggleHSR = new SubQuestion()
            {
                Question = "Hours or distance?",
                Description = "",
                QuestionType = QuestionType.toggle,
                AnswerOptions = ["Hours", "Distance"],
                Answer = "Distance",
                SubQuestionKey = V1SubQuestionKeys.travel_hours_or_distance,
            };
            highSpeedRail.SubQuestions.Add(hoursDistanceToggleHSR);

            DisplayRule hoursDisplayRuleHSR = new DisplayRule()
            {
                SubQuestion = hoursDistanceToggleHSR,
                ValidValues = ["Hours"]
            };

            DisplayRule distanceDisplayRuleHSR = new DisplayRule()
            {
                SubQuestion = hoursDistanceToggleHSR,
                ValidValues = ["Distance"]
            };

            SubQuestion hoursInputHSR = new SubQuestion()
            {
                Question = "How many hours do you travel per year?",
                Description = "",
                QuestionType = QuestionType.intInput,
                Answer = "",
                UnitOptions = ["hours"],
                SelectedUnit = "hours",
                DefaultMetricUnit = "hours",
                DefaultImperialUnit = "hours",
                DisplayRules = [hoursDisplayRuleHSR],
                SubQuestionKey = V1SubQuestionKeys.travel_hours_travelled,
            };
            highSpeedRail.SubQuestions.Add(hoursInputHSR);
            highSpeedRail.DisplaySubQuestions.Add(hoursInputHSR);

            SubQuestion distanceInputHSR = new SubQuestion()
            {
                Question = "How much do you travel per year?",
                Description = "",
                QuestionType = QuestionType.intInput,
                Answer = "",
                UnitOptions = ["km", "mi"],
                DefaultMetricUnit = "km",
                DefaultImperialUnit = "mi",
                DisplayRules = [distanceDisplayRuleHSR],
                SubQuestionKey = V1SubQuestionKeys.travel_distance_travelled,
            };
            highSpeedRail.SubQuestions.Add(distanceInputHSR);
            highSpeedRail.DisplaySubQuestions.Add(distanceInputHSR);

            return highSpeedRail;
        }

        StandardQuestionOption createRegularRailQuestionOption()
        {
            StandardQuestionOption regularRail = new StandardQuestionOption()
            {
                Name = "Regular Rail",
                IsSelected = false,
                Tags = ["travel", "train"],
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion hoursDistanceToggleRR = new SubQuestion()
            {
                Question = "Hours or distance?",
                Description = "",
                QuestionType = QuestionType.toggle,
                AnswerOptions = ["Hours", "Distance"],
                Answer = "Distance",
                SubQuestionKey = V1SubQuestionKeys.travel_hours_or_distance,
            };
            regularRail.SubQuestions.Add(hoursDistanceToggleRR);

            DisplayRule hoursDisplayRuleRR = new DisplayRule()
            {
                SubQuestion = hoursDistanceToggleRR,
                ValidValues = ["Hours"]
            };

            DisplayRule distanceDisplayRuleRR = new DisplayRule()
            {
                SubQuestion = hoursDistanceToggleRR,
                ValidValues = ["Distance"]
            };

            SubQuestion hoursInputRR = new SubQuestion()
            {
                Question = "How many hours do you travel per year?",
                Description = "",
                QuestionType = QuestionType.intInput,
                Answer = "",
                UnitOptions = ["hours"],
                SelectedUnit = "hours",
                DefaultMetricUnit = "hours",
                DefaultImperialUnit = "hours",
                DisplayRules = [hoursDisplayRuleRR],
                SubQuestionKey = V1SubQuestionKeys.travel_hours_travelled,
            };
            regularRail.SubQuestions.Add(hoursInputRR);
            regularRail.DisplaySubQuestions.Add(hoursInputRR);

            SubQuestion distanceInputRR = new SubQuestion()
            {
                Question = "How much do you travel per year?",
                Description = "",
                QuestionType = QuestionType.intInput,
                Answer = "",
                UnitOptions = ["km", "mi"],
                DefaultMetricUnit = "km",
                DefaultImperialUnit = "mi",
                DisplayRules = [distanceDisplayRuleRR],
                SubQuestionKey = V1SubQuestionKeys.travel_distance_travelled,
            };
            regularRail.SubQuestions.Add(distanceInputRR);
            regularRail.DisplaySubQuestions.Add(distanceInputRR);

            return regularRail;
        }
    }

    public Question CreateBusTravelQuestion()
    {
        Question question = new Question()
        {
            QuestionName = "How much do you travel by bus per year?",
            Description = "You can answer in distance travelled or hours travelled.",
            Tips = [],
            AllowCustomOptions = false,
            AllowReusableQuestionOptions = false,
            QuestionOptions = [],
            ReusableQuestionOptionsTags = ["travel", "bus"],
        };

        StandardQuestionOption regularBus = new StandardQuestionOption()
        {
            Name = "Regular Diesel Bus",
            IsSelected = false,
            Tags = ["travel", "bus"],
            SubQuestions = [],
            DisplaySubQuestions = [],
        };

        SubQuestion hoursDistanceToggleBus = new SubQuestion()
        {
            Question = "Hours or distance?",
            Description = "",
            QuestionType = QuestionType.toggle,
            AnswerOptions = ["Hours", "Distance"],
            Answer = "Distance",
            SubQuestionKey = V1SubQuestionKeys.travel_hours_or_distance,
        };
        regularBus.SubQuestions.Add(hoursDistanceToggleBus);

        DisplayRule hoursDisplayRuleBus = new DisplayRule()
        {
            SubQuestion = hoursDistanceToggleBus,
            ValidValues = ["Hours"]
        };

        DisplayRule distanceDisplayRuleBus = new DisplayRule()
        {
            SubQuestion = hoursDistanceToggleBus,
            ValidValues = ["Distance"]
        };

        SubQuestion hoursInputBus = new SubQuestion()
        {
            Question = "How many hours do you travel per year?",
            Description = "",
            QuestionType = QuestionType.intInput,
            Answer = "",
            UnitOptions = ["hours"],
            SelectedUnit = "hours",
            DefaultMetricUnit = "hours",
            DefaultImperialUnit = "hours",
            DisplayRules = [hoursDisplayRuleBus],
            SubQuestionKey = V1SubQuestionKeys.travel_hours_travelled,
        };
        regularBus.SubQuestions.Add(hoursInputBus);
        regularBus.DisplaySubQuestions.Add(hoursInputBus);

        SubQuestion distanceInputBus = new SubQuestion()
        {
            Question = "How much do you travel per year?",
            Description = "",
            QuestionType = QuestionType.intInput,
            Answer = "",
            UnitOptions = ["km", "mi"],
            DefaultMetricUnit = "km",
            DefaultImperialUnit = "mi",
            DisplayRules = [distanceDisplayRuleBus],
            SubQuestionKey = V1SubQuestionKeys.travel_distance_travelled,
        };
        regularBus.SubQuestions.Add(distanceInputBus);
        regularBus.DisplaySubQuestions.Add(distanceInputBus);

        question.QuestionOptions.Add(regularBus);

        return question;
    }

    public Question CreateCruiseQuestion()
    {
        Question question = new Question()
        {
            QuestionName = "How much do you travel by cruise per year?",
            Description = "You can answer in distance travelled.",
            Tips = [],
            AllowCustomOptions = false,
            AllowReusableQuestionOptions = false,
            QuestionOptions = [],
            ReusableQuestionOptionsTags = ["travel", "cruise"],
        };

        StandardQuestionOption regularCruise = new StandardQuestionOption()
        {
            Name = "Regular Cruise",
            IsSelected = false,
            Tags = ["travel", "cruise"],
            SubQuestions = [],
            DisplaySubQuestions = [],
        };

        SubQuestion distanceInputCruise = new SubQuestion()
        {
            Question = "How much do you travel per year?",
            Description = "",
            QuestionType = QuestionType.intInput,
            Answer = "",
            UnitOptions = ["km", "mi"],
            DefaultMetricUnit = "km",
            DefaultImperialUnit = "mi",
            SubQuestionKey = V1SubQuestionKeys.travel_distance_travelled,
        };
        regularCruise.SubQuestions.Add(distanceInputCruise);
        regularCruise.DisplaySubQuestions.Add(distanceInputCruise);

        question.QuestionOptions.Add(regularCruise);

        return question;
    }

    public Question CreateFerryQuestion()
    {
        Question question = new Question()
        {
            QuestionName = "How much do you travel by ferry per year?",
            Description = "As a foot passenger, with a car or as an average passenger. You can answer in distance travelled or hours travelled.",
            Tips = [],
            AllowCustomOptions = false,
            AllowReusableQuestionOptions = false,
            QuestionOptions = [],
            ReusableQuestionOptionsTags = ["travel", "ferry"],
        };

        question.QuestionOptions.Add(createAveragePassengerOption());
        question.QuestionOptions.Add(createFootPassengerOption());
        question.QuestionOptions.Add(createCarPassengerOption());

        return question;

        StandardQuestionOption createAveragePassengerOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption()
            {
                Name = "Average passenger",
                IsSelected = false,
                Tags = ["travel", "ferry"],
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion hoursDistanceToggleSubQuestion = new SubQuestion()
            {
                Question = "Hours or distance?",
                Description = "",
                QuestionType = QuestionType.toggle,
                AnswerOptions = ["Hours", "Distance"],
                Answer = "Distance",
                SubQuestionKey = V1SubQuestionKeys.travel_hours_or_distance,
            };
            questionOption.SubQuestions.Add(hoursDistanceToggleSubQuestion);

            DisplayRule hoursDisplayRule = new DisplayRule()
            {
                SubQuestion = hoursDistanceToggleSubQuestion,
                ValidValues = ["Hours"]
            };

            DisplayRule distanceDisplayRule = new DisplayRule()
            {
                SubQuestion = hoursDistanceToggleSubQuestion,
                ValidValues = ["Distance"]
            };

            SubQuestion hoursInputSubQuestion = new SubQuestion()
            {
                Question = "How many hours do you travel per year?",
                Description = "",
                QuestionType = QuestionType.intInput,
                Answer = "",
                UnitOptions = ["hours"],
                SelectedUnit = "hours",
                DefaultMetricUnit = "hours",
                DefaultImperialUnit = "hours",
                DisplayRules = [hoursDisplayRule],
                SubQuestionKey = V1SubQuestionKeys.travel_hours_travelled,
            };
            questionOption.SubQuestions.Add(hoursInputSubQuestion);
            questionOption.DisplaySubQuestions.Add(hoursInputSubQuestion);

            SubQuestion distanceInputSubQuestion = new SubQuestion()
            {
                Question = "How much do you travel per year?",
                Description = "",
                QuestionType = QuestionType.intInput,
                Answer = "",
                UnitOptions = ["km", "mi"],
                DefaultMetricUnit = "km",
                DefaultImperialUnit = "mi",
                DisplayRules = [distanceDisplayRule],
                SubQuestionKey = V1SubQuestionKeys.travel_distance_travelled,
            };
            questionOption.SubQuestions.Add(distanceInputSubQuestion);
            questionOption.DisplaySubQuestions.Add(distanceInputSubQuestion);

            return questionOption;
        }

        StandardQuestionOption createFootPassengerOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption()
            {
                Name = "Foot passenger",
                IsSelected = false,
                Tags = ["travel", "ferry"],
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion hoursDistanceToggleSubQuestion = new SubQuestion()
            {
                Question = "Hours or distance?",
                Description = "",
                QuestionType = QuestionType.toggle,
                AnswerOptions = ["Hours", "Distance"],
                Answer = "Distance",
                SubQuestionKey = V1SubQuestionKeys.travel_hours_or_distance,
            };
            questionOption.SubQuestions.Add(hoursDistanceToggleSubQuestion);

            DisplayRule hoursDisplayRule = new DisplayRule()
            {
                SubQuestion = hoursDistanceToggleSubQuestion,
                ValidValues = ["Hours"]
            };

            DisplayRule distanceDisplayRule = new DisplayRule()
            {
                SubQuestion = hoursDistanceToggleSubQuestion,
                ValidValues = ["Distance"]
            };

            SubQuestion hoursInputSubQuestion = new SubQuestion()
            {
                Question = "How many hours do you travel per year?",
                Description = "",
                QuestionType = QuestionType.intInput,
                Answer = "",
                UnitOptions = ["hours"],
                SelectedUnit = "hours",
                DefaultMetricUnit = "hours",
                DefaultImperialUnit = "hours",
                DisplayRules = [hoursDisplayRule],
                SubQuestionKey = V1SubQuestionKeys.travel_hours_travelled,
            };
            questionOption.SubQuestions.Add(hoursInputSubQuestion);
            questionOption.DisplaySubQuestions.Add(hoursInputSubQuestion);

            SubQuestion distanceInputSubQuestion = new SubQuestion()
            {
                Question = "How much do you travel per year?",
                Description = "",
                QuestionType = QuestionType.intInput,
                Answer = "",
                UnitOptions = ["km", "mi"],
                DefaultMetricUnit = "km",
                DefaultImperialUnit = "mi",
                DisplayRules = [distanceDisplayRule],
                SubQuestionKey = V1SubQuestionKeys.travel_distance_travelled,
            };
            questionOption.SubQuestions.Add(distanceInputSubQuestion);
            questionOption.DisplaySubQuestions.Add(distanceInputSubQuestion);

            return questionOption;
        }

        StandardQuestionOption createCarPassengerOption()
        {

            StandardQuestionOption questionOption = new StandardQuestionOption()
            {
                Name = "Car passenger",
                IsSelected = false,
                Tags = ["travel", "ferry"],
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion hoursDistanceToggleSubQuestion = new SubQuestion()
            {
                Question = "Hours or distance?",
                Description = "",
                QuestionType = QuestionType.toggle,
                AnswerOptions = ["Hours", "Distance"],
                Answer = "Distance",
                SubQuestionKey = V1SubQuestionKeys.travel_hours_or_distance,
            };
            questionOption.SubQuestions.Add(hoursDistanceToggleSubQuestion);

            DisplayRule hoursDisplayRule = new DisplayRule()
            {
                SubQuestion = hoursDistanceToggleSubQuestion,
                ValidValues = ["Hours"]
            };

            DisplayRule distanceDisplayRule = new DisplayRule()
            {
                SubQuestion = hoursDistanceToggleSubQuestion,
                ValidValues = ["Distance"]
            };

            SubQuestion hoursInputSubQuestion = new SubQuestion()
            {
                Question = "How many hours do you travel per year?",
                Description = "",
                QuestionType = QuestionType.intInput,
                Answer = "",
                UnitOptions = ["hours"],
                SelectedUnit = "hours",
                DefaultMetricUnit = "hours",
                DefaultImperialUnit = "hours",
                DisplayRules = [hoursDisplayRule],
                SubQuestionKey = V1SubQuestionKeys.travel_hours_travelled,
            };
            questionOption.SubQuestions.Add(hoursInputSubQuestion);
            questionOption.DisplaySubQuestions.Add(hoursInputSubQuestion);

            SubQuestion distanceInputSubQuestion = new SubQuestion()
            {
                Question = "How much do you travel per year?",
                Description = "",
                QuestionType = QuestionType.intInput,
                Answer = "",
                UnitOptions = ["km", "mi"],
                DefaultMetricUnit = "km",
                DefaultImperialUnit = "mi",
                DisplayRules = [distanceDisplayRule],
                SubQuestionKey = V1SubQuestionKeys.travel_distance_travelled,
            };
            questionOption.SubQuestions.Add(distanceInputSubQuestion);
            questionOption.DisplaySubQuestions.Add(distanceInputSubQuestion);

            return questionOption;
        }
    }
    //!SECTION[epic=Travel] - Travel

    //SECTION[epic=Home] - Home
    public void CreateAndAddHomeSurveyCategory(SurveyModel surveyModel)
    {
        SurveyCategory home = new SurveyCategory()
        {
            CategoryName = "Home",
            Description = "Electricity usage, heating, cooling, Hot water and cooking",
            Questions = [],
        };

        home.Questions.Add(CreateFlightsQuestion());
        home.Questions.Add(CreateCarTravelQuestion());
        home.Questions.Add(CreateTrainTravelQuestion());
        home.Questions.Add(CreateBusTravelQuestion());
        home.Questions.Add(CreateCruiseQuestion());
        home.Questions.Add(CreateFerryQuestion());

        //Add mobility SurveyCategory to surveyModel
        surveyModel.SurveyCategories.Add(home);
    }

    public Question CreateElectricityQuestion()
    {
        Question question = new Question()
        {
            QuestionName = "How much do you use per year and where does it come from?",
            Description = "Select the electricity sources you use and enter how much you electricity you use from those sources.",
            Tips = [],
            AllowCustomOptions = false,
            AllowReusableQuestionOptions = false,
            QuestionOptions = [],
            ReusableQuestionOptionsTags = ["home", "electricity"],
        };

        question.QuestionOptions.Add(createNationalGridQuestionOption());
        question.QuestionOptions.Add(createRenewableQuestionOption());
        question.QuestionOptions.Add(createSolarQuestionOption());
        question.QuestionOptions.Add(createSolarBatteryQuestionOption());

        return question;

        QuestionOption createNationalGridQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "National grid",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["kWh", "GJ"],
                DefaultMetricUnit = "kWh",
                DefaultImperialUnit = "kWh",
                AverageValueRoute = "home/electricity/national-grid",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }

        QuestionOption createRenewableQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Renewables",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["kWh", "GJ"],
                DefaultMetricUnit = "kWh",
                DefaultImperialUnit = "kWh",
                AverageValueRoute = "home/electricity/renewables",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }

        QuestionOption createSolarQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Solar",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["kWh", "GJ"],
                DefaultMetricUnit = "kWh",
                DefaultImperialUnit = "kWh",
                AverageValueRoute = "home/electricity/solar",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }

        QuestionOption createSolarBatteryQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Solar + battery",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["kWh", "GJ"],
                DefaultMetricUnit = "kWh",
                DefaultImperialUnit = "kWh",
                AverageValueRoute = "home/electricity/solar-battery",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }
    }

    public Question CreateHeatingQuestion()
    {
        Question question = new Question()
        {
            QuestionName = "How do you heat your home and how much energy do you use?",
            Description = "Select the heating sources you use and enter how much you energy you use from those sources.",
            Tips = [],
            AllowCustomOptions = false,
            AllowReusableQuestionOptions = false,
            QuestionOptions = [],
            ReusableQuestionOptionsTags = ["home", "heating"],
        };

        question.QuestionOptions.Add(createNaturalGasQuestionOption());
        question.QuestionOptions.Add(createHeatingOilQuestionOption());
        question.QuestionOptions.Add(createElectricQuestionOption());
        question.QuestionOptions.Add(createHeatPumpGroundQuestionOption());
        question.QuestionOptions.Add(createHeatPumpAirQuestionOption());
        question.QuestionOptions.Add(createWoodPelletsQuestionOption());
        question.QuestionOptions.Add(createWoodQuestionOption());
        question.QuestionOptions.Add(createGeothermalQuestionOption());

        return question;

        QuestionOption createNaturalGasQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Natural gas",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["m3", "ft3", "kWh", "GJ"],
                DefaultMetricUnit = "m3",
                DefaultImperialUnit = "ft3",
                AverageValueRoute = "home/heating/natural-gas",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }

        QuestionOption createHeatingOilQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Heating oil",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["BTU", "kWh", "GJ"],
                DefaultMetricUnit = "kWh",
                DefaultImperialUnit = "kWh",
                AverageValueRoute = "home/heating/heating-oil",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }

        QuestionOption createElectricQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Electric",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["kWh", "GJ"],
                DefaultMetricUnit = "kWh",
                DefaultImperialUnit = "kWh",
                AverageValueRoute = "home/heating/electric",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }

        QuestionOption createHeatPumpGroundQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Ground source heat pump",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["kWh", "GJ"],
                DefaultMetricUnit = "kWh",
                DefaultImperialUnit = "kWh",
                AverageValueRoute = "home/heating/gshp",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }

        QuestionOption createHeatPumpAirQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Air source heat pump",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["kWh", "GJ"],
                DefaultMetricUnit = "kWh",
                DefaultImperialUnit = "kWh",
                AverageValueRoute = "home/heating/ashp",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }

        QuestionOption createWoodPelletsQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Wood pellets",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["m3", "ft3", "tonne", "kWh", "GJ"],
                DefaultMetricUnit = "tonne",
                DefaultImperialUnit = "tonne",
                AverageValueRoute = "home/heating/wood-pellets",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }

        QuestionOption createWoodQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Wood",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["m3", "ft3", "tonne", "kWh", "GJ"],
                DefaultMetricUnit = "tonne",
                DefaultImperialUnit = "tonne",
                AverageValueRoute = "home/heating/wood",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }

        QuestionOption createGeothermalQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Geothermal heat",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["kWh", "GJ"],
                DefaultMetricUnit = "kWh",
                DefaultImperialUnit = "kWh",
                AverageValueRoute = "home/heating/geothermal",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }
    }

    public Question CreateHotWaterQuestion()
    {
        Question question = new Question()
        {
            QuestionName = "How do you heat your water and how much energy do you use?",
            Description = "Select the heating sources you use and enter how much you energy you use from those sources.",
            Tips = [],
            AllowCustomOptions = false,
            AllowReusableQuestionOptions = false,
            QuestionOptions = [],
            ReusableQuestionOptionsTags = ["home", "hot water"],
        };

        question.QuestionOptions.Add(createNaturalGasQuestionOption());
        question.QuestionOptions.Add(createHeatingOilQuestionOption());
        question.QuestionOptions.Add(createElectricQuestionOption());
        question.QuestionOptions.Add(createHeatPumpGroundQuestionOption());
        question.QuestionOptions.Add(createHeatPumpAirQuestionOption());
        question.QuestionOptions.Add(createWoodPelletsQuestionOption());
        question.QuestionOptions.Add(createWoodQuestionOption());
        question.QuestionOptions.Add(createGeothermalQuestionOption());

        return question;

        QuestionOption createNaturalGasQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Natural gas",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["m3", "ft3", "kWh", "GJ"],
                DefaultMetricUnit = "m3",
                DefaultImperialUnit = "ft3",
                AverageValueRoute = "home/heating/natural-gas",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }

        QuestionOption createHeatingOilQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Heating oil",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["BTU", "kWh", "GJ"],
                DefaultMetricUnit = "kWh",
                DefaultImperialUnit = "kWh",
                AverageValueRoute = "home/heating/heating-oil",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }

        QuestionOption createElectricQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Electric",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["kWh", "GJ"],
                DefaultMetricUnit = "kWh",
                DefaultImperialUnit = "kWh",
                AverageValueRoute = "home/heating/electric",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }

        QuestionOption createHeatPumpGroundQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Ground source heat pump",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["kWh", "GJ"],
                DefaultMetricUnit = "kWh",
                DefaultImperialUnit = "kWh",
                AverageValueRoute = "home/heating/gshp",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }

        QuestionOption createHeatPumpAirQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Air source heat pump",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["kWh", "GJ"],
                DefaultMetricUnit = "kWh",
                DefaultImperialUnit = "kWh",
                AverageValueRoute = "home/heating/ashp",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }

        QuestionOption createWoodPelletsQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Wood pellets",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["m3", "ft3", "tonne", "kWh", "GJ"],
                DefaultMetricUnit = "tonne",
                DefaultImperialUnit = "tonne",
                AverageValueRoute = "home/heating/wood-pellets",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }

        QuestionOption createWoodQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Wood",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["m3", "ft3", "tonne", "kWh", "GJ"],
                DefaultMetricUnit = "tonne",
                DefaultImperialUnit = "tonne",
                AverageValueRoute = "home/heating/wood",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }

        QuestionOption createGeothermalQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Geothermal heat",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion usageSubQuestion = new SubQuestion()
            {
                Question = "Annual usage",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["kWh", "GJ"],
                DefaultMetricUnit = "kWh",
                DefaultImperialUnit = "kWh",
                AverageValueRoute = "home/heating/geothermal",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(usageSubQuestion);
            questionOption.DisplaySubQuestions.Add(usageSubQuestion);

            return questionOption;
        }
    }

    public Question CreateCoolingQuestion()
    {
        Question question = new Question()
        {
            QuestionName = "Do you use air-conditioning and how much A/C do you use?",
            Description = "Select Air-conditioning if you use it and enter how much you use it.",
            Tips = [],
            AllowCustomOptions = false,
            AllowReusableQuestionOptions = false,
            QuestionOptions = [],
            ReusableQuestionOptionsTags = ["home", "cooling"],
        };

        StandardQuestionOption questionOption = new StandardQuestionOption
        {
            Name = "Air conditioning",
            Tags = [],
            IsSelected = false,
            SubQuestions = [],
            DisplaySubQuestions = [],
        };

        SubQuestion usageSubQuestion = new SubQuestion()
        {
            Question = "Annual usage",
            Description = "",
            QuestionType = QuestionType.doubleInputOrAvg,
            Answer = "",
            UnitOptions = ["kWh", "GJ"],
            DefaultMetricUnit = "kWh",
            DefaultImperialUnit = "kWh",
            AverageValueRoute = "home/cooling/air-conditioning",
            SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
        };
        questionOption.SubQuestions.Add(usageSubQuestion);
        questionOption.DisplaySubQuestions.Add(usageSubQuestion);

        question.QuestionOptions.Add(questionOption);

        return question;
    }

    public Question CreateCookingQuestion()
    {

        Question question = new Question()
        {
            QuestionName = "Which appliances do you use to cook and how much do you use them?",
            Description = "Select the appliances you use and enter how much you use them.",
            Tips = [],
            AllowCustomOptions = false,
            AllowReusableQuestionOptions = false,
            QuestionOptions = [],
            ReusableQuestionOptionsTags = ["home", "cooking"],
        };

        question.QuestionOptions.Add(createGasCooktopQuestionOption());
        question.QuestionOptions.Add(createElectricCooktopQuestionOption());
        question.QuestionOptions.Add(createInductionCooktopQuestionOption());
        question.QuestionOptions.Add(createOvenQuestionOption());
        question.QuestionOptions.Add(createAirfryerQuestionOption());
        question.QuestionOptions.Add(createSlowCookerQuestionOption());
        question.QuestionOptions.Add(createMicrowaveQuestionOption());

        return question;

        QuestionOption createGasCooktopQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Gas cooktop",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion timePerUseSubQuestion = new SubQuestion()
            {
                Question = "Time per use",
                Description = "How long, on average, do you use your gas cooktop? In minutes",
                QuestionType = QuestionType.doubleInput,
                Answer = "",
                UnitOptions = ["minutes"],
                DefaultMetricUnit = "minutes",
                DefaultImperialUnit = "minutes",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(timePerUseSubQuestion);
            questionOption.DisplaySubQuestions.Add(timePerUseSubQuestion);

            SubQuestion weeklyUsageSubQuestion = new SubQuestion()
            {
                Question = "Weekly usage",
                Description = "How many times per week do you use your cooktop?",
                QuestionType = QuestionType.intInput,
                Answer = "",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(weeklyUsageSubQuestion);
            questionOption.DisplaySubQuestions.Add(weeklyUsageSubQuestion);

            return questionOption;
        }

        QuestionOption createElectricCooktopQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Electric cooktop",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion timePerUseSubQuestion = new SubQuestion()
            {
                Question = "Time per use",
                Description = "How long, on average, do you use your electric cooktop? In minutes",
                QuestionType = QuestionType.doubleInput,
                Answer = "",
                UnitOptions = ["minutes"],
                DefaultMetricUnit = "minutes",
                DefaultImperialUnit = "minutes",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(timePerUseSubQuestion);
            questionOption.DisplaySubQuestions.Add(timePerUseSubQuestion);

            SubQuestion weeklyUsageSubQuestion = new SubQuestion()
            {
                Question = "Weekly usage",
                Description = "How many times per week do you use your cooktop?",
                QuestionType = QuestionType.intInput,
                Answer = "",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(weeklyUsageSubQuestion);
            questionOption.DisplaySubQuestions.Add(weeklyUsageSubQuestion);

            return questionOption;
        }

        QuestionOption createInductionCooktopQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Induction cooktop",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion timePerUseSubQuestion = new SubQuestion()
            {
                Question = "Time per use",
                Description = "How long, on average, do you use your induction cooktop? In minutes",
                QuestionType = QuestionType.doubleInput,
                Answer = "",
                UnitOptions = ["minutes"],
                DefaultMetricUnit = "minutes",
                DefaultImperialUnit = "minutes",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(timePerUseSubQuestion);
            questionOption.DisplaySubQuestions.Add(timePerUseSubQuestion);

            SubQuestion weeklyUsageSubQuestion = new SubQuestion()
            {
                Question = "Weekly usage",
                Description = "How many times per week do you use your cooktop?",
                QuestionType = QuestionType.intInput,
                Answer = "",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(weeklyUsageSubQuestion);
            questionOption.DisplaySubQuestions.Add(weeklyUsageSubQuestion);

            return questionOption;
        }

        QuestionOption createOvenQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Oven",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion timePerUseSubQuestion = new SubQuestion()
            {
                Question = "Time per use",
                Description = "How long, on average, do you use your oven? In minutes",
                QuestionType = QuestionType.doubleInput,
                Answer = "",
                UnitOptions = ["minutes"],
                DefaultMetricUnit = "minutes",
                DefaultImperialUnit = "minutes",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(timePerUseSubQuestion);
            questionOption.DisplaySubQuestions.Add(timePerUseSubQuestion);

            SubQuestion weeklyUsageSubQuestion = new SubQuestion()
            {
                Question = "Weekly usage",
                Description = "How many times per week do you use your oven?",
                QuestionType = QuestionType.intInput,
                Answer = "",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(weeklyUsageSubQuestion);
            questionOption.DisplaySubQuestions.Add(weeklyUsageSubQuestion);

            return questionOption;
        }

        QuestionOption createAirfryerQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Airfryer",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion timePerUseSubQuestion = new SubQuestion()
            {
                Question = "Time per use",
                Description = "How long, on average, do you use your airfryer? In minutes",
                QuestionType = QuestionType.doubleInput,
                Answer = "",
                UnitOptions = ["minutes"],
                DefaultMetricUnit = "minutes",
                DefaultImperialUnit = "minutes",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(timePerUseSubQuestion);
            questionOption.DisplaySubQuestions.Add(timePerUseSubQuestion);

            SubQuestion weeklyUsageSubQuestion = new SubQuestion()
            {
                Question = "Weekly usage",
                Description = "How many times per week do you use your airfryer?",
                QuestionType = QuestionType.intInput,
                Answer = "",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(weeklyUsageSubQuestion);
            questionOption.DisplaySubQuestions.Add(weeklyUsageSubQuestion);

            return questionOption;
        }

        QuestionOption createSlowCookerQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Slow cooker",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion timePerUseSubQuestion = new SubQuestion()
            {
                Question = "Time per use",
                Description = "How long, on average, do you use your slow cooker? In minutes",
                QuestionType = QuestionType.doubleInput,
                Answer = "",
                UnitOptions = ["minutes"],
                DefaultMetricUnit = "minutes",
                DefaultImperialUnit = "minutes",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(timePerUseSubQuestion);
            questionOption.DisplaySubQuestions.Add(timePerUseSubQuestion);

            SubQuestion weeklyUsageSubQuestion = new SubQuestion()
            {
                Question = "Weekly usage",
                Description = "How many times per week do you use your slow cooker?",
                QuestionType = QuestionType.intInput,
                Answer = "",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(weeklyUsageSubQuestion);
            questionOption.DisplaySubQuestions.Add(weeklyUsageSubQuestion);

            return questionOption;
        }

        QuestionOption createMicrowaveQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Microwave",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion timePerUseSubQuestion = new SubQuestion()
            {
                Question = "Time per use",
                Description = "How long, on average, do you use your microwave? In minutes",
                QuestionType = QuestionType.doubleInput,
                Answer = "",
                UnitOptions = ["minutes"],
                DefaultMetricUnit = "minutes",
                DefaultImperialUnit = "minutes",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(timePerUseSubQuestion);
            questionOption.DisplaySubQuestions.Add(timePerUseSubQuestion);

            SubQuestion weeklyUsageSubQuestion = new SubQuestion()
            {
                Question = "Weekly usage",
                Description = "How many times per week do you use your microwave?",
                QuestionType = QuestionType.intInput,
                Answer = "",
                SubQuestionKey = V1SubQuestionKeys.home_annual_usage,
            };
            questionOption.SubQuestions.Add(weeklyUsageSubQuestion);
            questionOption.DisplaySubQuestions.Add(weeklyUsageSubQuestion);

            return questionOption;
        }

    }
    //!SECTION[epic=Home] - Home

    //SECTION[epic=Food] - Food
    public void CreateAndAddFoodSurveyCategory(SurveyModel surveyModel)
    {
        SurveyCategory food = new SurveyCategory
        {
            CategoryName = "Food",
            Description = "The food you eat",
            Questions = [],
        };

        food.Questions.Add(createMeatFishAndTofuQuestion());
        food.Questions.Add(createDairyAndEggsQuestion());
        food.Questions.Add(createFruitsAndVegetablesQuestion());
        food.Questions.Add(createGrainProductsQuestion());
        food.Questions.Add(createOtherFoodsQuestion());

        surveyModel.SurveyCategories.Add(food);
    }

    public Question createMeatFishAndTofuQuestion()
    {

        Question question = new Question()
        {
            QuestionName = "How much meat, fish and tofu do you eat?",
            Description = "Select the foods you eat and enter how much and how often you eat them.",
            Tips = [],
            AllowCustomOptions = false,
            AllowReusableQuestionOptions = false,
            QuestionOptions = [],
            ReusableQuestionOptionsTags = ["food", "meat-fish-tofu"],
        };

        question.QuestionOptions.Add(createBeefQuestionOption());
        question.QuestionOptions.Add(createLambAndMuttonQuestionOption());
        question.QuestionOptions.Add(createPorkQuestionOption());
        question.QuestionOptions.Add(createPoultryQuestionOption());
        question.QuestionOptions.Add(createShellfishFarmedQuestionOption());
        question.QuestionOptions.Add(createShellfishWildQuestionOption());
        question.QuestionOptions.Add(createFishFarmedQuestionOption());
        question.QuestionOptions.Add(createFishWildQuestionOption());
        question.QuestionOptions.Add(createTofuQuestionOption());
        question.QuestionOptions.Add(createSeitanQuestionOption());

        return question;

        QuestionOption createBeefQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Beef",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/meat-fish-tofu/beef",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/meat-fish-tofu/beef",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createLambAndMuttonQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Lamb and Mutton",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/meat-fish-tofu/lamb-mutton",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/meat-fish-tofu/lamb-mutton",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createPorkQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Pork",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/meat-fish-tofu/pork",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/meat-fish-tofu/pork",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createPoultryQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Poultry",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/meat-fish-tofu/poultry",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/meat-fish-tofu/poultry",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createShellfishFarmedQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Shellfish farmed",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/meat-fish-tofu/shellfish-farmed",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/meat-fish-tofu/shellfish-farmed",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createShellfishWildQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Shellfish wild",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/meat-fish-tofu/shellfish-wild",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/meat-fish-tofu/shellfish-wild",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createFishFarmedQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Fish farmed",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/meat-fish-tofu/fish-farmed",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/meat-fish-tofu/fish-farmed",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createFishWildQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Fish wild",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/meat-fish-tofu/fish-wild",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/meat-fish-tofu/fish-wild",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createTofuQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Tofu",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/meat-fish-tofu/tofu",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/meat-fish-tofu/tofu",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createSeitanQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Seitan",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/meat-fish-tofu/seitan",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/meat-fish-tofu/seitan",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }
    }

    public Question createDairyAndEggsQuestion()
    {
        Question question = new Question()
        {
            QuestionName = "How much dairy and eggs do you eat?",
            Description = "Select the foods you eat and enter how much and how often you eat them.",
            Tips = [],
            AllowCustomOptions = false,
            AllowReusableQuestionOptions = false,
            QuestionOptions = [],
            ReusableQuestionOptionsTags = ["food", "dairy-eggs"],
        };

        question.QuestionOptions.Add(createCheeseQuestionOption());
        question.QuestionOptions.Add(createMilkQuestionOption());
        question.QuestionOptions.Add(createCreamQuestionOption());
        question.QuestionOptions.Add(createButterQuestionOption());
        question.QuestionOptions.Add(createSoymilkQuestionOption());
        question.QuestionOptions.Add(createEggsQuestionOption());

        return question;

        QuestionOption createCheeseQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Cheese",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/dairy-eggs/cheese",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/dairy-eggs/cheese",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createMilkQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Milk",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["cl", "fl oz"],
                DefaultMetricUnit = "cl",
                DefaultImperialUnit = "fl oz",
                AverageValueRoute = "food/dairy-eggs/milk",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/dairy-eggs/milk",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createCreamQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Cream",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["cl", "fl oz"],
                DefaultMetricUnit = "cl",
                DefaultImperialUnit = "fl oz",
                AverageValueRoute = "food/dairy-eggs/cream",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/dairy-eggs/cream",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createButterQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Butter",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/dairy-eggs/butter",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/dairy-eggs/butter",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createSoymilkQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Soymilk",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["cl", "fl oz"],
                DefaultMetricUnit = "cl",
                DefaultImperialUnit = "fl oz",
                AverageValueRoute = "food/dairy-eggs/soymilk",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/dairy-eggs/soymilk",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createEggsQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Eggs",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["eggs", "g", "oz"],
                DefaultMetricUnit = "eggs",
                DefaultImperialUnit = "eggs",
                AverageValueRoute = "food/dairy-eggs/eggs",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/dairy-eggs/eggs",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }
    }

    public Question createFruitsAndVegetablesQuestion()
    {
        Question question = new Question()
        {
            QuestionName = "How much fruit and vegetables do you eat?",
            Description = "Select the foods you eat and enter how much and how often you eat them.",
            Tips = [],
            AllowCustomOptions = false,
            AllowReusableQuestionOptions = false,
            QuestionOptions = [],
            ReusableQuestionOptionsTags = ["food", "fruit-vegetables"],
        };

        question.QuestionOptions.Add(createCitrusFruitsQuestionOption());
        question.QuestionOptions.Add(createBerriesAndGrapesQuestionOption());
        question.QuestionOptions.Add(createBananasQuestionOption());
        question.QuestionOptions.Add(createOtherFruitQuestionOption());
        question.QuestionOptions.Add(createPulsesQuestionOption());
        question.QuestionOptions.Add(createPeasQuestionOption());
        question.QuestionOptions.Add(createPotatoesQuestionOption());
        question.QuestionOptions.Add(createUnionsAndLeeksQuestionOption());
        question.QuestionOptions.Add(createRootVegetablesQuestionOption());
        question.QuestionOptions.Add(createTomatoesQuestionOption());
        question.QuestionOptions.Add(createCornQuestionOption());
        question.QuestionOptions.Add(createOtherVegetablesQuestionOption());

        return question;

        QuestionOption createCitrusFruitsQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Citrus fruits",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["pieces", "g", "oz"],
                DefaultMetricUnit = "pieces",
                DefaultImperialUnit = "pieces",
                AverageValueRoute = "food/fruit-vegetables/citrus",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/fruit-vegetables/citrus",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createBerriesAndGrapesQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Berries and grapes",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/fruit-vegetables/berries-grapes",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/fruit-vegetables/berries-grapes",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createBananasQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Bananas",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["bananas", "g", "oz"],
                DefaultMetricUnit = "bananas",
                DefaultImperialUnit = "bananas",
                AverageValueRoute = "food/fruit-vegetables/bananas",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/fruit-vegetables/bananas",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createOtherFruitQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Other fruit",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/fruit-vegetables/other-fruit",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/fruit-vegetables/other-fruit",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createPulsesQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Pulses",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/fruit-vegetables/pulses",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/fruit-vegetables/pulses",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createPeasQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Peas",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/fruit-vegetables/peas",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/fruit-vegetables/peas",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createPotatoesQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Potatoes",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/fruit-vegetables/potatoes",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/fruit-vegetables/potatoes",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createUnionsAndLeeksQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Unions and leeks",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/fruit-vegetables/unions-leeks",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/fruit-vegetables/unions-leeks",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createRootVegetablesQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Root vegetables",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/fruit-vegetables/root-vegetables",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/fruit-vegetables/root-vegetables",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createTomatoesQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Tomatoes",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/fruit-vegetables/tomatoes",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/fruit-vegetables/tomatoes",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createCornQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Corn",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/fruit-vegetables/corn",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/fruit-vegetables/corn",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }

        QuestionOption createOtherVegetablesQuestionOption()
        {
            StandardQuestionOption questionOption = new StandardQuestionOption
            {
                Name = "Other vegetables",
                Tags = [],
                IsSelected = false,
                SubQuestions = [],
                DisplaySubQuestions = [],
            };

            SubQuestion portionSizeSubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                UnitOptions = ["g", "oz"],
                DefaultMetricUnit = "g",
                DefaultImperialUnit = "oz",
                AverageValueRoute = "food/fruit-vegetables/other-vegetables",
                SubQuestionKey = V1SubQuestionKeys.food_portion_size,
            };
            questionOption.SubQuestions.Add(portionSizeSubQuestion);
            questionOption.DisplaySubQuestions.Add(portionSizeSubQuestion);

            SubQuestion portionFrequencySubQuestion = new SubQuestion()
            {
                Question = "Average portion size",
                Description = "",
                QuestionType = QuestionType.doubleInputOrAvg,
                Answer = "",
                AverageValueRoute = "food/fruit-vegetables/other-vegetables",
                SubQuestionKey = V1SubQuestionKeys.food_weekly_frequency,
            };
            questionOption.SubQuestions.Add(portionFrequencySubQuestion);
            questionOption.DisplaySubQuestions.Add(portionFrequencySubQuestion);

            return questionOption;
        }
    }

    public Question createGrainProductsQuestion()
    {

    }

    public Question createOtherFoodsQuestion()
    {

    }
    //!SECTION[epic=Food] - Food

    //SECTION[epic=Purchasing habits] - Purchasing habits
    public void CreateAndAddPurchasingtySurveyCategory(SurveyModel surveyModel)
    {
        //TODO: Create SurveyCategory
        //TODO: Create Questions
        //TODO: Create QuestionOptions
        //TODO: Create SubQuestions for QuestionOptions
        //TODO: Create DisplayRules for SubQuestions

        //TODO: Add Purchasing SurveyCategory to surveyModel
    }
    //!SECTION[epic=Purchasing habits] - Purchasing habits
}