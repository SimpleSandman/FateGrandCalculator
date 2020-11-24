using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using FateGrandOrderPOC.Test.Fixture;

namespace FateGrandOrderPOC.Test.Utility
{
    public static class WireMockUtility
    {
        public static readonly string REGION = "NA";

        public static readonly string IMAGINARY_ELEMENT_CE = "9400280";
        public static readonly string KSCOPE_CE = "9400340";
        public static readonly string HOLY_NIGHT_SUPPER_CE = "9402090";
        public static readonly string AERIAL_DRIVE_CE = "9402750";

        public static readonly string GILGAMESH_ARCHER = "200200";
        public static readonly string ARASH_ARCHER = "201300";
        public static readonly string ASTOLFO_RIDER = "400400";
        public static readonly string WAVER_CASTER = "501900";
        public static readonly string SKADI_CASTER = "503900";
        public static readonly string JACK_ASSASSIN = "600500";
        public static readonly string LANCELOT_BERSERKER = "700200";
        public static readonly string SPARTACUS_BERSERKER = "700500";
        public static readonly string RAIKOU_BERSERKER = "702300";
        public static readonly string DANTES_AVENGER = "1100200";

        public static readonly string PLUGSUIT_ID = "20";
        public static readonly string FRAGMENT_2004_ID = "100";
        public static readonly string ARTIC_ID = "110";

        /// <summary>
        /// Add all the WireMock stubs
        /// </summary>
        public static void AddStubs(WireMockFixture wiremockFixture)
        {
            AddExportStubs(wiremockFixture);
            AddServantStubs(wiremockFixture);
            AddCraftEssenceStubs(wiremockFixture);
            AddMysticCodeStubs(wiremockFixture);
        }

        #region Private Methods
        /// <summary>
        /// Create constant game data endpoints as WireMock stubs
        /// </summary>
        private static void AddExportStubs(WireMockFixture wiremockFixture)
        {
            const string CONSTANT_RATE_JSON = "NiceConstant.json";
            const string CLASS_ATTACK_RATE_JSON = "NiceClassAttackRate.json";
            const string CLASS_RELATION_JSON = "NiceClassRelation.json";
            const string ATTRIBUTE_RELATION_JSON = "NiceAttributeRelation.json";

            // build necessary export mock responses
            ConstantNiceJson mockConstantRateResponse = LoadTestData.DeserializeExportJson<ConstantNiceJson>(REGION, CONSTANT_RATE_JSON);
            LoadTestData.CreateExportWireMockStub(wiremockFixture, REGION, CONSTANT_RATE_JSON, mockConstantRateResponse);

            ClassAttackRateNiceJson mockClassAttackRateResponse = LoadTestData.DeserializeExportJson<ClassAttackRateNiceJson>(REGION, CLASS_ATTACK_RATE_JSON);
            LoadTestData.CreateExportWireMockStub(wiremockFixture, REGION, CLASS_ATTACK_RATE_JSON, mockClassAttackRateResponse);

            ClassRelationNiceJson mockClassRelationResponse = LoadTestData.DeserializeExportJson<ClassRelationNiceJson>(REGION, CLASS_RELATION_JSON);
            LoadTestData.CreateExportWireMockStub(wiremockFixture, REGION, CLASS_RELATION_JSON, mockClassRelationResponse);

            AttributeRelationNiceJson mockAttributeRelationResponse = LoadTestData.DeserializeExportJson<AttributeRelationNiceJson>(REGION, ATTRIBUTE_RELATION_JSON);
            LoadTestData.CreateExportWireMockStub(wiremockFixture, REGION, ATTRIBUTE_RELATION_JSON, mockAttributeRelationResponse);
        }

        /// <summary>
        /// Create servant game data endpoints as WireMock stubs
        /// </summary>
        private static void AddServantStubs(WireMockFixture wiremockFixture)
        {
            // build mock servant responses
            ServantNiceJson mockResponse = LoadTestData.DeserializeServantJson(REGION, "Avenger", $"{DANTES_AVENGER}-EdmondDantesAvenger.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, REGION, "servant", DANTES_AVENGER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(REGION, "Caster", $"{SKADI_CASTER}-ScathachSkadiCaster.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, REGION, "servant", SKADI_CASTER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(REGION, "Berserker", $"{LANCELOT_BERSERKER}-LancelotBerserker.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, REGION, "servant", LANCELOT_BERSERKER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(REGION, "Caster", $"{WAVER_CASTER}-ZhugeLiangCaster.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, REGION, "servant", WAVER_CASTER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(REGION, "Berserker", $"{SPARTACUS_BERSERKER}-SpartacusBerserker.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, REGION, "servant", SPARTACUS_BERSERKER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(REGION, "Archer", $"{GILGAMESH_ARCHER}-GilgameshArcher.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, REGION, "servant", GILGAMESH_ARCHER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(REGION, "Archer", $"{ARASH_ARCHER}-ArashArcher.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, REGION, "servant", ARASH_ARCHER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(REGION, "Berserker", $"{RAIKOU_BERSERKER}-RaikouBerserker.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, REGION, "servant", RAIKOU_BERSERKER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(REGION, "Rider", $"{ASTOLFO_RIDER}-AstolfoRider.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, REGION, "servant", ASTOLFO_RIDER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(REGION, "Assassin", $"{JACK_ASSASSIN}-JackAssassin.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, REGION, "servant", JACK_ASSASSIN, mockResponse);
        }

        /// <summary>
        /// Create craft essence game data endpoints as WireMock stubs
        /// </summary>
        private static void AddCraftEssenceStubs(WireMockFixture wiremockFixture)
        {
            // build mock craft essence response
            EquipNiceJson mockResponse = LoadTestData.DeserializeCraftEssenceJson(REGION, $"{KSCOPE_CE}-Kaleidoscope.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, REGION, "equip", KSCOPE_CE, mockResponse);

            mockResponse = LoadTestData.DeserializeCraftEssenceJson(REGION, $"{IMAGINARY_ELEMENT_CE}-ImaginaryElement.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, REGION, "equip", IMAGINARY_ELEMENT_CE, mockResponse);

            mockResponse = LoadTestData.DeserializeCraftEssenceJson(REGION, $"{AERIAL_DRIVE_CE}-AerialDrive.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, REGION, "equip", AERIAL_DRIVE_CE, mockResponse);

            mockResponse = LoadTestData.DeserializeCraftEssenceJson(REGION, $"{HOLY_NIGHT_SUPPER_CE}-HolyNightSupper.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, REGION, "equip", HOLY_NIGHT_SUPPER_CE, mockResponse);
        }

        /// <summary>
        /// Create mystic code game data endpoints as WireMock stubs
        /// </summary>
        private static void AddMysticCodeStubs(WireMockFixture wiremockFixture)
        {
            // build mock mystic code response
            MysticCodeNiceJson mockResponse = LoadTestData.DeserializeMysticCodeJson(REGION, $"{ARTIC_ID}-Artic.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, REGION, "MC", ARTIC_ID, mockResponse);

            mockResponse = LoadTestData.DeserializeMysticCodeJson(REGION, $"{PLUGSUIT_ID}-CombatUniform.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, REGION, "MC", PLUGSUIT_ID, mockResponse);

            mockResponse = LoadTestData.DeserializeMysticCodeJson(REGION, $"{FRAGMENT_2004_ID}-Fragment2004.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, REGION, "MC", FRAGMENT_2004_ID, mockResponse);
        }
        #endregion
    }
}
