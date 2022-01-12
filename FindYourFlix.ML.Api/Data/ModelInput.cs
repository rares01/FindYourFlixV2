using Microsoft.ML.Data;

namespace FindYourFlix.ML.Api.Data
{
    public class ModelInput
    {
        [ColumnName(@"userId")]
        public float UserId { get; set; }

        [ColumnName(@"movieId")]
        public float MovieId { get; set; }
    }
}
