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

    //SECTION - Mobility
    public void CreateAndAddMobilitySurveyCategory(SurveyModel surveyModel)
    {
        SurveyCategory mobility = new SurveyCategory()
        {
            CategoryName = "Mobility",
            Description = "Cars, bikes, taxi's, public transit and frequent journeys. Not including travel.",
            Questions = []
        };

        //SECTION - Cars question
        //TODO - Create Question
        //TODO - Create QuestionOptions and/or QuestionOptionTemplates
        //TODO - Create SubQuestions for QuestionOption(templates)
        //TODO - Create DisplayRules for SubQuestions
        //TODO - Add Question to SurveyCategory

        //SECTION - Motorbikes question

        //SECTION - Bikes & E-bikes question

        //SECTION - Taxi's and Rideshare question

        //SECTION - Bus question

        //SECTION - Tram and subway question

        //SECTION - Rail question

        //SECTION - Frequent Journey question
        //TODO: Figure out how to do this one

        //Add mobility SurveyCategory to surveyModel
        surveyModel.SurveyCategories.Add(mobility);
    }

    public void CreateAndAddTravelSurveyCategory(SurveyModel surveyModel)
    {
        //TODO: Create SurveyCategory
        //TODO: Create Questions
        //TODO: Create QuestionOptions and QuestionOptionTemplates
        //TODO: Create SubQuestions for QuestionOptions/QuestionOptionTemplates
        //TODO: Create DisplayRules for SubQuestions

        //TODO: Add Travel SurveyCategory to surveyModel
    }

    public void CreateAndAddHomeSurveyCategory(SurveyModel surveyModel)
    {
        //TODO: Create SurveyCategory
        //TODO: Create Questions
        //TODO: Create QuestionOptions
        //TODO: Create SubQuestions for QuestionOptions
        //TODO: Create DisplayRules for SubQuestions

        //TODO: Add Home SurveyCategory to surveyModel
    }

    public void CreateAndAddFoodSurveyCategory(SurveyModel surveyModel)
    {
        //TODO: Create SurveyCategory
        //TODO: Create Questions
        //TODO: Create QuestionOptions
        //TODO: Create SubQuestions for QuestionOptions
        //TODO: Create DisplayRules for SubQuestions

        //TODO: Add Food SurveyCategory to surveyModel
    }

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