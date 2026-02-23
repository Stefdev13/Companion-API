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

        QuestionOption carQuestionOptiontemplate = new StandardQuestionOption()
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
        carQuestion.OptionTemplate = carQuestionOptiontemplate;

        SubQuestion carUsage = new SubQuestion()
        {
            Question = "How much do you drive per year?",
            Description = "In kilometers or miles",
            QuestionType = QuestionType.intInput,
            UnitOptions = ["km", "mi"],
            DefaultMetricUnit = "km",
            DefaultImperialUnit = "mi",
            Answer = "",
            SubQuestionKey = "car-usage",
        };
        carQuestionOptiontemplate.DisplaySubQuestions.Add(carUsage);

        SubQuestion firstSecondHand = new SubQuestion()
        {
            Question = "Is the car first or second hand?",
            Description = "",
            QuestionType = QuestionType.toggle,
            AnswerOptions = ["First hand", "Second hand"],
            Answer = "",
            SubQuestionKey = "first-or-second-hand",
        };
        carQuestionOptiontemplate.DisplaySubQuestions.Add(firstSecondHand);

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
            SubQuestionKey = "car-ownership",
        };
        carQuestionOptiontemplate.DisplaySubQuestions.Add(carOwnership);

        SubQuestion carType = new SubQuestion()
        {
            Question = "What kind of car is it?",
            Description = "Petrol, diesel, hybrid, plug-in hybrid or electric",
            QuestionType = QuestionType.select,
            AnswerOptions = ["Petrol", "Diesel", "Hybrid", "Plug-in hybrid", "Electric"],
            Answer = "",
            SubQuestionKey = "car-type",
        };
        carQuestionOptiontemplate.DisplaySubQuestions.Add(carType);

        SubQuestion carSize = new SubQuestion()
        {
            Question = "How big is the car?",
            Description = "Small, medium or large",
            QuestionType = QuestionType.select,
            AnswerOptions = ["Small", "Medium", "Large"],
            Answer = "",
            SubQuestionKey = "car-size",
        };
        carQuestionOptiontemplate.DisplaySubQuestions.Add(carSize);

        SubQuestion usageOrManufacturerRatings = new SubQuestion()
        {
            Question = "Personal fuel consumption or manufacturers figures?",
            Description = "Would you like to use your own fuel comsumption figures or use the manufacturers figures?",
            QuestionType = QuestionType.toggle,
            AnswerOptions = ["Personal", "Manufacturer"],
            Answer = "",
            SubQuestionKey = "consumption-or-manufacturer",
        };
        carQuestionOptiontemplate.DisplaySubQuestions.Add(usageOrManufacturerRatings);

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
            SubQuestionKey = "emissions-figure",
        };
        carQuestionOptiontemplate.DisplaySubQuestions.Add(carEmissionsFigure);

        DisplayRule personalUsageDisplayRule = new DisplayRule()
        {
            SubQuestion = usageOrManufacturerRatings,
            ValidValues = ["Personal"]
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
            DisplayRules = [personalUsageDisplayRule],
            SubQuestionKey = "fuel-consumption",
        };
        carQuestionOptiontemplate.DisplaySubQuestions.Add(carFuelConsumption);

        SubQuestion carKWhConsumption = new SubQuestion()
        {
            Question = "Energy consumption",
            Description = "How efficient is your car? In kWh/100km or mi/kWh.",
            QuestionType = QuestionType.doubleInput,
            UnitOptions = ["kWh/100km", "mi/kWh"],
            DefaultMetricUnit = "kWh/100km",
            DefaultImperialUnit = "mi/kWh",
            Answer = "",
            DisplayRules = [personalUsageDisplayRule],
            SubQuestionKey = "kwh-consumption",
        };
        carQuestionOptiontemplate.DisplaySubQuestions.Add(carKWhConsumption);

        DisplayRule electricOrPHEVDisplayRule = new DisplayRule()
        {
            SubQuestion = carType,
            ValidValues = ["Plug-in hybrid", "Electric"]
        };

        SubQuestion carBatterySize = new SubQuestion()
        {
            Question = "Energy consumption",
            Description = "How efficient is your car? In kWh/100km or mi/kWh.",
            QuestionType = QuestionType.doubleInputOrAvg,
            UnitOptions = ["kWh/100km", "mi/kWh"],
            DefaultMetricUnit = "kWh/100km",
            DefaultImperialUnit = "mi/kWh",
            Answer = "",
            DisplayRules = [electricOrPHEVDisplayRule],
            SubQuestionKey = "battery-size",
        };
        carQuestionOptiontemplate.DisplaySubQuestions.Add(carBatterySize);

        SubQuestion carChargeSource = new SubQuestion()
        {
            Question = "Charging source",
            Description = "What percentage of the electricity used by your car came from renewable sources?",
            QuestionType = QuestionType.percentage,
            Answer = "",
            DisplayRules = [electricOrPHEVDisplayRule],
            SubQuestionKey = "kwh-consumption",
        };
        carQuestionOptiontemplate.DisplaySubQuestions.Add(carChargeSource);

        return carQuestion;
    }

    public Question CreateMotorbikeQuestion()
    {

    }

    public Question CreateBikesQuestion()
    {

    }

    public Question CreateTaxiQuestion()
    {

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