using FateGrandCalculator.AtlasAcademy.Json;

using FateGrandCalculator.Test.Utility.Fixture;

namespace FateGrandCalculator.Test.Utility
{
    public class WireMockUtility
    {
        public static readonly string IMAGINARY_ELEMENT_CE = "9400280";
        public static readonly string KSCOPE_CE = "9400340";
        public static readonly string GOLDEN_SUMO_CE = "9401640";
        public static readonly string HOLY_NIGHT_SUPPER_CE = "9402090";
        public static readonly string AERIAL_DRIVE_CE = "9402750";

        public static readonly string TOMOE_GOZEN_SABER = "104500";
        public static readonly string NERO_CLAUDIUS_BRIDE = "100600";
        public static readonly string GILGAMESH_ARCHER = "200200";
        public static readonly string ARASH_ARCHER = "201300";
        public static readonly string PARVATI_LANCER = "303000";
        public static readonly string VALKYRIE_LANCER = "303300";
        public static readonly string ASTOLFO_RIDER = "400400";
        public static readonly string MERLIN_CASTER = "500800";
        public static readonly string NITOCRIS_CASTER = "501200";
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

        private readonly string _langSuffix;
        private readonly string _region;

        public WireMockUtility(string langSuffix)
        {
            _region = langSuffix;
            _langSuffix = langSuffix == "NA" ? "" : "EN";
        }

        /// <summary>
        /// Add all the WireMock stubs
        /// </summary>
        public void AddStubs(WireMockFixture wiremockFixture)
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
        private void AddExportStubs(WireMockFixture wiremockFixture)
        {
            const string CONSTANT_RATE_JSON = "NiceConstant.json";
            const string CLASS_ATTACK_RATE_JSON = "NiceClassAttackRate.json";
            const string CLASS_RELATION_JSON = "NiceClassRelation.json";
            const string ATTRIBUTE_RELATION_JSON = "NiceAttributeRelation.json";
            const string BASIC_SERVANT_JSON = "basic_servant.json";
            const string BASIC_EQUIP_JSON = "basic_equip.json";
            const string SVT_GRAIL_COST_NICE_JSON = "NiceSvtGrailCost.json";

            // build necessary export mock responses
            ConstantNiceJson mockConstantRateResponse = LoadTestData.DeserializeExportJson<ConstantNiceJson>(_region, CONSTANT_RATE_JSON);
            LoadTestData.CreateExportWireMockStub(wiremockFixture, _region, CONSTANT_RATE_JSON, mockConstantRateResponse);

            ClassAttackRateNiceJson mockClassAttackRateResponse = LoadTestData.DeserializeExportJson<ClassAttackRateNiceJson>(_region, CLASS_ATTACK_RATE_JSON);
            LoadTestData.CreateExportWireMockStub(wiremockFixture, _region, CLASS_ATTACK_RATE_JSON, mockClassAttackRateResponse);

            ClassRelationNiceJson mockClassRelationResponse = LoadTestData.DeserializeExportJson<ClassRelationNiceJson>(_region, CLASS_RELATION_JSON);
            LoadTestData.CreateExportWireMockStub(wiremockFixture, _region, CLASS_RELATION_JSON, mockClassRelationResponse);

            AttributeRelationNiceJson mockAttributeRelationResponse = LoadTestData.DeserializeExportJson<AttributeRelationNiceJson>(_region, ATTRIBUTE_RELATION_JSON);
            LoadTestData.CreateExportWireMockStub(wiremockFixture, _region, ATTRIBUTE_RELATION_JSON, mockAttributeRelationResponse);

            ServantBasicJsonCollection mockServantBasicJsonResponse = LoadTestData.DeserializeExportJson<ServantBasicJsonCollection>(_region, BASIC_SERVANT_JSON);
            LoadTestData.CreateExportWireMockStub(wiremockFixture, _region, BASIC_SERVANT_JSON, mockServantBasicJsonResponse);

            EquipBasicJsonCollection mockEquipBasicJsonResponse = LoadTestData.DeserializeExportJson<EquipBasicJsonCollection>(_region, BASIC_EQUIP_JSON);
            LoadTestData.CreateExportWireMockStub(wiremockFixture, _region, BASIC_EQUIP_JSON, mockEquipBasicJsonResponse);

            GrailCostNiceJson mockSvtGrailCostNiceResponse = LoadTestData.DeserializeExportJson<GrailCostNiceJson>(_region, SVT_GRAIL_COST_NICE_JSON);
            LoadTestData.CreateExportWireMockStub(wiremockFixture, _region, SVT_GRAIL_COST_NICE_JSON, mockSvtGrailCostNiceResponse);
        }

        /// <summary>
        /// Create servant game data endpoints as WireMock stubs
        /// </summary>
        private void AddServantStubs(WireMockFixture wiremockFixture)
        {
            // build mock servant responses
            ServantNiceJson mockResponse = LoadTestData.DeserializeServantJson(_region, "Archer", $"{ARASH_ARCHER}-Arash{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "servant", ARASH_ARCHER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(_region, "Archer", $"{GILGAMESH_ARCHER}-Gilgamesh{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "servant", GILGAMESH_ARCHER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(_region, "Assassin", $"{JACK_ASSASSIN}-Jack{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "servant", JACK_ASSASSIN, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(_region, "Avenger", $"{DANTES_AVENGER}-EdmondDantes{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "servant", DANTES_AVENGER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(_region, "Berserker", $"{LANCELOT_BERSERKER}-Lancelot{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "servant", LANCELOT_BERSERKER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(_region, "Berserker", $"{RAIKOU_BERSERKER}-Raikou{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "servant", RAIKOU_BERSERKER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(_region, "Berserker", $"{SPARTACUS_BERSERKER}-Spartacus{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "servant", SPARTACUS_BERSERKER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(_region, "Caster", $"{MERLIN_CASTER}-Merlin{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "servant", MERLIN_CASTER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(_region, "Caster", $"{NITOCRIS_CASTER}-Nitocris{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "servant", NITOCRIS_CASTER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(_region, "Caster", $"{SKADI_CASTER}-ScathachSkadi{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "servant", SKADI_CASTER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(_region, "Caster", $"{WAVER_CASTER}-ZhugeLiang{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "servant", WAVER_CASTER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(_region, "Lancer", $"{PARVATI_LANCER}-Parvati{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "servant", PARVATI_LANCER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(_region, "Lancer", $"{VALKYRIE_LANCER}-Valkyrie{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "servant", VALKYRIE_LANCER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(_region, "Rider", $"{ASTOLFO_RIDER}-Astolfo{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "servant", ASTOLFO_RIDER, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(_region, "Saber", $"{NERO_CLAUDIUS_BRIDE}-NeroClaudiusBride{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "servant", NERO_CLAUDIUS_BRIDE, mockResponse);

            mockResponse = LoadTestData.DeserializeServantJson(_region, "Saber", $"{TOMOE_GOZEN_SABER}-TomoeGozen{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "servant", TOMOE_GOZEN_SABER, mockResponse);
        }

        /// <summary>
        /// Create craft essence game data endpoints as WireMock stubs
        /// </summary>
        private void AddCraftEssenceStubs(WireMockFixture wiremockFixture)
        {
            // build mock craft essence response
            EquipNiceJson mockResponse = LoadTestData.DeserializeCraftEssenceJson(_region, $"{KSCOPE_CE}-Kaleidoscope{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "equip", KSCOPE_CE, mockResponse);

            mockResponse = LoadTestData.DeserializeCraftEssenceJson(_region, $"{IMAGINARY_ELEMENT_CE}-ImaginaryElement{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "equip", IMAGINARY_ELEMENT_CE, mockResponse);

            mockResponse = LoadTestData.DeserializeCraftEssenceJson(_region, $"{AERIAL_DRIVE_CE}-AerialDrive{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "equip", AERIAL_DRIVE_CE, mockResponse);

            mockResponse = LoadTestData.DeserializeCraftEssenceJson(_region, $"{HOLY_NIGHT_SUPPER_CE}-HolyNightSupper{_langSuffix}.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "equip", HOLY_NIGHT_SUPPER_CE, mockResponse);
        }

        /// <summary>
        /// Create mystic code game data endpoints as WireMock stubs
        /// </summary>
        private void AddMysticCodeStubs(WireMockFixture wiremockFixture)
        {
            // build mock mystic code response
            MysticCodeNiceJson mockResponse = LoadTestData.DeserializeMysticCodeJson(_region, $"{ARTIC_ID}-Artic.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "MC", ARTIC_ID, mockResponse);

            mockResponse = LoadTestData.DeserializeMysticCodeJson(_region, $"{PLUGSUIT_ID}-CombatUniform.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "MC", PLUGSUIT_ID, mockResponse);

            mockResponse = LoadTestData.DeserializeMysticCodeJson(_region, $"{FRAGMENT_2004_ID}-Fragment2004.json");
            LoadTestData.CreateNiceWireMockStub(wiremockFixture, _region, "MC", FRAGMENT_2004_ID, mockResponse);
        }
        #endregion
    }
}
