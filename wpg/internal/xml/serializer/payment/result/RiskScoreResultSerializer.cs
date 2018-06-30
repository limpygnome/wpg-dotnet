namespace Worldpay.@internal.xml.serializer.payment.result
{
    internal class RiskScoreResultSerializer
    {

        public static RiskScoreResult read(XmlBuilder builder)
        {
            RiskScoreResult result = null;
            if (builder.hasE("riskScore"))
            {
                int? value = builder.aToIntegerOptional("value");
                string provider = builder.a("provider");
                string id = builder.a("id");
                int? finalScore = builder.aToIntegerOptional("finalScore");
                string riskGuardianId = builder.a("RGID");
                int? totalScore = builder.aToIntegerOptional("tScore");
                int? totalRisk = builder.aToIntegerOptional("tRisk");
                string message = builder.a("message");
                string extendedResponse = builder.a("extendedResponse");

                result = new RiskScoreResult(id, provider, value, finalScore, totalScore, totalRisk, riskGuardianId, message, extendedResponse);
                builder.up();
            }
            return result;
        }

    }
}
