using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Plank.Models
{
    public class FiveMinutesPlankModelCreator
    {
        public static ObservableCollection<FiveMinutesPlankModel> CreateFiveMinutesPlankList()
        {
            var models = new ObservableCollection<FiveMinutesPlankModel>
            {
                CreateStartModel(),
                CreateRecoverTimeModel("fmpPrepareForFullPlank.png"),
                CreateAModel("Full Plank", 60, "fmpFullPlank.jpg"),
                CreateRecoverTimeModel("fmpPrepareForElbowPlank.png"),
                CreateAModel("Elbow Plank", 30, "fmpElbowPlank.jpg"),
                CreateRecoverTimeModel("fmpPrepareForRaisedLegPlank.png"),
                CreateAModel("Raised Leg Plank - Left", 30, "fmpRaisedLegPlank.jpg"),
                CreateRecoverTimeModel("fmpPrepareForRaisedLegPlank.png"),
                CreateAModel("Raised Leg Plank - Right", 30, "fmpRaisedLegPlank.jpg"),
                CreateRecoverTimeModel("fmpPrepareForSidePlank.png"),
                CreateAModel("Side Plank - Left", 30, "fmpSidePlank.jpg"),
                CreateRecoverTimeModel("fmpPrepareForSidePlank.png"),
                CreateAModel("Side Plank - Right", 30, "fmpSidePlank.jpg"),
                CreateRecoverTimeModel("fmpPrepareForFullPlank.png"),
                CreateAModel("Full Plank", 30, "fmpFullPlank.jpg"),
                CreateRecoverTimeModel("fmpPrepareForElbowPlank.png"),
                CreateAModel("Elbow Plank", 60, "fmpElbowPlank.jpg"),
                CreateEndModel()
            };

            return models;
        }

        private static FiveMinutesPlankModel CreateStartModel()
        {
            return CreateAModel("START", 1, "fmpAreYouReady.png", BgColorType.Waiting);
        }

        private static FiveMinutesPlankModel CreateEndModel()
        {
            return CreateAModel("END", 3, "fmpWellDone.png", BgColorType.Waiting);
        }

        private static FiveMinutesPlankModel CreateRecoverTimeModel(string imageUrl)
        {
            return CreateAModel("PREPARE & RECOVER", 2, imageUrl, BgColorType.Waiting);
        }

        private static FiveMinutesPlankModel CreateAModel(
            string title,
            int timeInSeconds,
            string imageUrl = "",
            BgColorType bgColorType = 0)
        {
            return FiveMinutesPlankModel.Create(title, timeInSeconds, imageUrl, bgColorType);
        }
    }
}