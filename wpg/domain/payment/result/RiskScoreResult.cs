using System;
namespace wpg.domain.payment.result
{
    public class RiskScoreResult
    {
        public RiskScoreResult(String id, String provider, int? value, int? finalScore, int? totalScore, int? totalRisk, String riskGuardianId, String message, String extendedResponse)
        {
            this.Id = id;
            this.Provider = provider;
            this.Value = value;
            this.FinalScore = finalScore;
            this.TotalScore = totalScore;
            this.TotalRisk = totalRisk;
            this.RiskGuardianId = riskGuardianId;
            this.Message = message;
            this.ExtendedResponse = extendedResponse;
        }

        public String Id { get; set; }
        public String Provider { get; set; }
        public int? Value { get; set; }
        public int? FinalScore { get; set; }
        public int? TotalScore { get; set; }
        public int? TotalRisk { get; set; }
        public String RiskGuardianId { get; set; }
        public String Message { get; set; }
        public String ExtendedResponse { get; set; }

    }
}
