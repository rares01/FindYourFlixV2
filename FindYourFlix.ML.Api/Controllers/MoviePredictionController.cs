using FindYourFlix.ML.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;

namespace FindYourFlix.ML.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviePredictionController : ControllerBase
    {
        private readonly PredictionEnginePool<ModelInput, ModelOutput> predictionEngine;

        public MoviePredictionController(PredictionEnginePool<ModelInput, ModelOutput> predictionEngine )
        {
            this.predictionEngine = predictionEngine;
        }

        [HttpPost]
        public ActionResult<float> Predict([FromBody] ModelInput model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ModelOutput output = predictionEngine.Predict(modelName: "MoviePredictionModel", example: model);
            return Ok(output.Score);
        }
    }
}
