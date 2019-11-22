using System.Text.RegularExpressions;
using TrainingLog.DataAccess.Activity;

namespace TrainingLog.DataAccess.Common {
    public static class Helpers {

        public static string CleanSQLText(string sqlText) {

            if (string.IsNullOrEmpty(sqlText)) {
                return sqlText;
            }

            return Regex.Replace(sqlText, @"\r\n?|\n|\r|\t|\s+", " ");

        }

        public static int CalculateIntensity(IntensityParms intensityParms) {
            var intensity = 0;
            decimal activityIntensityFactor = 1;
            var activityDuration = string.Empty;
            var activitySetCount = 0;

            if (intensityParms.ActivityIntensityFactor.HasValue && intensityParms.ActivityIntensityFactor.Value > 0 ) {
                activityIntensityFactor = intensityParms.ActivityIntensityFactor.Value;
            }

            if (!string.IsNullOrEmpty(intensityParms.ActivityDuration)) {
                activityDuration = intensityParms.ActivityDuration;
            }

            if (intensityParms.ActivitySetCount.HasValue && intensityParms.ActivitySetCount.Value > 0) {
                activitySetCount = intensityParms.ActivitySetCount.Value;
            }

            intensity = decimal.ToInt32(activityIntensityFactor * activitySetCount);

            return intensity;
        }

    }


}