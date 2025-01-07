namespace Quill.Postgres
{
    public class PgType
    {
        public string Typname { get; set; } = string.Empty;
        public int Oid { get; set; } = 0;
    }

    public static class PgTypes
    {
        public static readonly List<PgType> PG_TYPES = new List<PgType>
        {
            new PgType {
                Typname = "bool",
                Oid = 16,
            },
            new PgType {
                Typname = "bytea",
                Oid = 17,
            },
            new PgType {
                Typname = "char",
                Oid = 18,
            },
            new PgType {
                Typname = "name",
                Oid = 19,
            },
            new PgType {
                Typname = "int8",
                Oid = 20,
            },
            new PgType {
                Typname = "int2",
                Oid = 21,
            },
            new PgType {
                Typname = "int2vector",
                Oid = 22,
            },
            new PgType {
                Typname = "int4",
                Oid = 23,
            },
            new PgType {
                Typname = "regproc",
                Oid = 24,
            },
            new PgType {
                Typname = "text",
                Oid = 25,
            },
            new PgType {
                Typname = "oid",
                Oid = 26,
            },
            new PgType {
                Typname = "tid",
                Oid = 27,
            },
            new PgType {
                Typname = "xid",
                Oid = 28,
            },
            new PgType {
                Typname = "cid",
                Oid = 29,
            },
            new PgType {
                Typname = "oidvector",
                Oid = 30,
            },
            new PgType {
                Typname = "pg_ddl_command",
                Oid = 32,
            },
            new PgType {
                Typname = "pg_type",
                Oid = 71,
            },
            new PgType {
                Typname = "pg_attribute",
                Oid = 75,
            },
            new PgType {
                Typname = "pg_proc",
                Oid = 81,
            },
            new PgType {
                Typname = "pg_class",
                Oid = 83,
            },
            new PgType {
                Typname = "json",
                Oid = 114,
            },
            new PgType {
                Typname = "xml",
                Oid = 142,
            },
            new PgType {
                Typname = "_xml",
                Oid = 143,
            },
            new PgType {
                Typname = "pg_node_tree",
                Oid = 194,
            },
            new PgType {
                Typname = "_json",
                Oid = 199,
            },
            new PgType {
                Typname = "_pg_type",
                Oid = 210,
            },
            new PgType {
                Typname = "table_am_handler",
                Oid = 269,
            },
            new PgType {
                Typname = "_pg_attribute",
                Oid = 270,
            },
            new PgType {
                Typname = "_xid8",
                Oid = 271,
            },
            new PgType {
                Typname = "_pg_proc",
                Oid = 272,
            },
            new PgType {
                Typname = "_pg_class",
                Oid = 273,
            },
            new PgType {
                Typname = "index_am_handler",
                Oid = 325,
            },
            new PgType {
                Typname = "point",
                Oid = 600,
            },
            new PgType {
                Typname = "lseg",
                Oid = 601,
            },
            new PgType {
                Typname = "path",
                Oid = 602,
            },
            new PgType {
                Typname = "box",
                Oid = 603,
            },
            new PgType {
                Typname = "polygon",
                Oid = 604,
            },
            new PgType {
                Typname = "line",
                Oid = 628,
            },
            new PgType {
                Typname = "_line",
                Oid = 629,
            },
            new PgType {
                Typname = "cidr",
                Oid = 650,
            },
            new PgType {
                Typname = "_cidr",
                Oid = 651,
            },
            new PgType {
                Typname = "float4",
                Oid = 700,
            },
            new PgType {
                Typname = "float8",
                Oid = 701,
            },
            new PgType {
                Typname = "unknown",
                Oid = 705,
            },
            new PgType {
                Typname = "circle",
                Oid = 718,
            },
            new PgType {
                Typname = "_circle",
                Oid = 719,
            },
            new PgType {
                Typname = "macaddr8",
                Oid = 774,
            },
            new PgType {
                Typname = "_macaddr8",
                Oid = 775,
            },
            new PgType {
                Typname = "money",
                Oid = 790,
            },
            new PgType {
                Typname = "_money",
                Oid = 791,
            },
            new PgType {
                Typname = "macaddr",
                Oid = 829,
            },
            new PgType {
                Typname = "inet",
                Oid = 869,
            },
            new PgType {
                Typname = "_bool",
                Oid = 1000,
            },
            new PgType {
                Typname = "_bytea",
                Oid = 1001,
            },
            new PgType {
                Typname = "_char",
                Oid = 1002,
            },
            new PgType {
                Typname = "_name",
                Oid = 1003,
            },
            new PgType {
                Typname = "_int2",
                Oid = 1005,
            },
            new PgType {
                Typname = "_int2vector",
                Oid = 1006,
            },
            new PgType {
                Typname = "_int4",
                Oid = 1007,
            },
            new PgType {
                Typname = "_regproc",
                Oid = 1008,
            },
            new PgType {
                Typname = "_text",
                Oid = 1009,
            },
            new PgType {
                Typname = "_tid",
                Oid = 1010,
            },
            new PgType {
                Typname = "_xid",
                Oid = 1011,
            },
            new PgType {
                Typname = "_cid",
                Oid = 1012,
            },
            new PgType {
                Typname = "_oidvector",
                Oid = 1013,
            },
            new PgType {
                Typname = "_bpchar",
                Oid = 1014,
            },
            new PgType {
                Typname = "_varchar",
                Oid = 1015,
            },
            new PgType {
                Typname = "_int8",
                Oid = 1016,
            },
            new PgType {
                Typname = "_point",
                Oid = 1017,
            },
            new PgType {
                Typname = "_lseg",
                Oid = 1018,
            },
            new PgType {
                Typname = "_path",
                Oid = 1019,
            },
            new PgType {
                Typname = "_box",
                Oid = 1020,
            },
            new PgType {
                Typname = "_float4",
                Oid = 1021,
            },
            new PgType {
                Typname = "_float8",
                Oid = 1022,
            },
            new PgType {
                Typname = "_polygon",
                Oid = 1027,
            },
            new PgType {
                Typname = "_oid",
                Oid = 1028,
            },
            new PgType {
                Typname = "aclitem",
                Oid = 1033,
            },
            new PgType {
                Typname = "_aclitem",
                Oid = 1034,
            },
            new PgType {
                Typname = "_macaddr",
                Oid = 1040,
            },
            new PgType {
                Typname = "_inet",
                Oid = 1041,
            },
            new PgType {
                Typname = "bpchar",
                Oid = 1042,
            },
            new PgType {
                Typname = "varchar",
                Oid = 1043,
            },
            new PgType {
                Typname = "date",
                Oid = 1082,
            },
            new PgType {
                Typname = "time",
                Oid = 1083,
            },
            new PgType {
                Typname = "timestamp",
                Oid = 1114,
            },
            new PgType {
                Typname = "_timestamp",
                Oid = 1115,
            },
            new PgType {
                Typname = "_date",
                Oid = 1182,
            },
            new PgType {
                Typname = "_time",
                Oid = 1183,
            },
            new PgType {
                Typname = "timestamptz",
                Oid = 1184,
            },
            new PgType {
                Typname = "_timestamptz",
                Oid = 1185,
            },
            new PgType {
                Typname = "interval",
                Oid = 1186,
            },
            new PgType {
                Typname = "_interval",
                Oid = 1187,
            },
            new PgType {
                Typname = "_numeric",
                Oid = 1231,
            },
            new PgType {
                Typname = "pg_database",
                Oid = 1248,
            },
            new PgType {
                Typname = "_cstring",
                Oid = 1263,
            },
            new PgType {
                Typname = "timetz",
                Oid = 1266,
            },
            new PgType {
                Typname = "_timetz",
                Oid = 1270,
            },
            new PgType {
                Typname = "bit",
                Oid = 1560,
            },
            new PgType {
                Typname = "_bit",
                Oid = 1561,
            },
            new PgType {
                Typname = "varbit",
                Oid = 1562,
            },
            new PgType {
                Typname = "_varbit",
                Oid = 1563,
            },
            new PgType {
                Typname = "numeric",
                Oid = 1700,
            },
            new PgType {
                Typname = "refcursor",
                Oid = 1790,
            },
            new PgType {
                Typname = "_refcursor",
                Oid = 2201,
            },
            new PgType {
                Typname = "regprocedure",
                Oid = 2202,
            },
            new PgType {
                Typname = "regoper",
                Oid = 2203,
            },
            new PgType {
                Typname = "regoperator",
                Oid = 2204,
            },
            new PgType {
                Typname = "regclass",
                Oid = 2205,
            },
            new PgType {
                Typname = "regtype",
                Oid = 2206,
            },
            new PgType {
                Typname = "_regprocedure",
                Oid = 2207,
            },
            new PgType {
                Typname = "_regoper",
                Oid = 2208,
            },
            new PgType {
                Typname = "_regoperator",
                Oid = 2209,
            },
            new PgType {
                Typname = "_regclass",
                Oid = 2210,
            },
            new PgType {
                Typname = "_regtype",
                Oid = 2211,
            },
            new PgType {
                Typname = "record",
                Oid = 2249,
            },
            new PgType {
                Typname = "cstring",
                Oid = 2275,
            },
            new PgType {
                Typname = "any",
                Oid = 2276,
            },
            new PgType {
                Typname = "anyarray",
                Oid = 2277,
            },
            new PgType {
                Typname = "void",
                Oid = 2278,
            },
            new PgType {
                Typname = "trigger",
                Oid = 2279,
            },
            new PgType {
                Typname = "language_handler",
                Oid = 2280,
            },
            new PgType {
                Typname = "internal",
                Oid = 2281,
            },
            new PgType {
                Typname = "anyelement",
                Oid = 2283,
            },
            new PgType {
                Typname = "_record",
                Oid = 2287,
            },
            new PgType {
                Typname = "anynonarray",
                Oid = 2776,
            },
            new PgType {
                Typname = "pg_authid",
                Oid = 2842,
            },
            new PgType {
                Typname = "pg_auth_members",
                Oid = 2843,
            },
            new PgType {
                Typname = "_txid_snapshot",
                Oid = 2949,
            },
            new PgType {
                Typname = "uuid",
                Oid = 2950,
            },
            new PgType {
                Typname = "_uuid",
                Oid = 2951,
            },
            new PgType {
                Typname = "txid_snapshot",
                Oid = 2970,
            },
            new PgType {
                Typname = "fdw_handler",
                Oid = 3115,
            },
            new PgType {
                Typname = "pg_lsn",
                Oid = 3220,
            },
            new PgType {
                Typname = "_pg_lsn",
                Oid = 3221,
            },
            new PgType {
                Typname = "tsm_handler",
                Oid = 3310,
            },
            new PgType {
                Typname = "pg_ndistinct",
                Oid = 3361,
            },
            new PgType {
                Typname = "pg_dependencies",
                Oid = 3402,
            },
            new PgType {
                Typname = "anyenum",
                Oid = 3500,
            },
            new PgType {
                Typname = "tsvector",
                Oid = 3614,
            },
            new PgType {
                Typname = "tsquery",
                Oid = 3615,
            },
            new PgType {
                Typname = "gtsvector",
                Oid = 3642,
            },
            new PgType {
                Typname = "_tsvector",
                Oid = 3643,
            },
            new PgType {
                Typname = "_gtsvector",
                Oid = 3644,
            },
            new PgType {
                Typname = "_tsquery",
                Oid = 3645,
            },
            new PgType {
                Typname = "regconfig",
                Oid = 3734,
            },
            new PgType {
                Typname = "_regconfig",
                Oid = 3735,
            },
            new PgType {
                Typname = "regdictionary",
                Oid = 3769,
            },
            new PgType {
                Typname = "_regdictionary",
                Oid = 3770,
            },
            new PgType {
                Typname = "jsonb",
                Oid = 3802,
            },
            new PgType {
                Typname = "_jsonb",
                Oid = 3807,
            },
            new PgType {
                Typname = "anyrange",
                Oid = 3831,
            },
            new PgType {
                Typname = "event_trigger",
                Oid = 3838,
            },
            new PgType {
                Typname = "int4range",
                Oid = 3904,
            },
            new PgType {
                Typname = "_int4range",
                Oid = 3905,
            },
            new PgType {
                Typname = "numrange",
                Oid = 3906,
            },
            new PgType {
                Typname = "_numrange",
                Oid = 3907,
            },
            new PgType {
                Typname = "tsrange",
                Oid = 3908,
            },
            new PgType {
                Typname = "_tsrange",
                Oid = 3909,
            },
            new PgType {
                Typname = "tstzrange",
                Oid = 3910,
            },
            new PgType {
                Typname = "_tstzrange",
                Oid = 3911,
            },
            new PgType {
                Typname = "daterange",
                Oid = 3912,
            },
            new PgType {
                Typname = "_daterange",
                Oid = 3913,
            },
            new PgType {
                Typname = "int8range",
                Oid = 3926,
            },
            new PgType {
                Typname = "_int8range",
                Oid = 3927,
            },
            new PgType {
                Typname = "pg_shseclabel",
                Oid = 4066,
            },
            new PgType {
                Typname = "jsonpath",
                Oid = 4072,
            },
            new PgType {
                Typname = "_jsonpath",
                Oid = 4073,
            },
            new PgType {
                Typname = "regnamespace",
                Oid = 4089,
            },
            new PgType {
                Typname = "_regnamespace",
                Oid = 4090,
            },
            new PgType {
                Typname = "regrole",
                Oid = 4096,
            },
            new PgType {
                Typname = "_regrole",
                Oid = 4097,
            },
            new PgType {
                Typname = "regcollation",
                Oid = 4191,
            },
            new PgType {
                Typname = "_regcollation",
                Oid = 4192,
            },
            new PgType {
                Typname = "int4multirange",
                Oid = 4451,
            },
            new PgType {
                Typname = "nummultirange",
                Oid = 4532,
            },
            new PgType {
                Typname = "tsmultirange",
                Oid = 4533,
            },
            new PgType {
                Typname = "tstzmultirange",
                Oid = 4534,
            },
            new PgType {
                Typname = "datemultirange",
                Oid = 4535,
            },
            new PgType {
                Typname = "int8multirange",
                Oid = 4536,
            },
            new PgType {
                Typname = "anymultirange",
                Oid = 4537,
            },
            new PgType {
                Typname = "anycompatiblemultirange",
                Oid = 4538,
            },
            new PgType {
                Typname = "pg_brin_bloom_summary",
                Oid = 4600,
            },
            new PgType {
                Typname = "pg_brin_minmax_multi_summary",
                Oid = 4601,
            },
            new PgType {
                Typname = "pg_mcv_list",
                Oid = 5017,
            },
            new PgType {
                Typname = "pg_snapshot",
                Oid = 5038,
            },
            new PgType {
                Typname = "_pg_snapshot",
                Oid = 5039,
            },
            new PgType {
                Typname = "xid8",
                Oid = 5069,
            },
            new PgType {
                Typname = "anycompatible",
                Oid = 5077,
            },
            new PgType {
                Typname = "anycompatiblearray",
                Oid = 5078,
            },
            new PgType {
                Typname = "anycompatiblenonarray",
                Oid = 5079,
            },
            new PgType {
                Typname = "anycompatiblerange",
                Oid = 5080,
            },
            new PgType {
                Typname = "pg_subscription",
                Oid = 6101,
            },
            new PgType {
                Typname = "_int4multirange",
                Oid = 6150,
            },
            new PgType {
                Typname = "_nummultirange",
                Oid = 6151,
            },
            new PgType {
                Typname = "_tsmultirange",
                Oid = 6152,
            },
            new PgType {
                Typname = "_tstzmultirange",
                Oid = 6153,
            },
            new PgType {
                Typname = "_datemultirange",
                Oid = 6155,
            },
            new PgType {
                Typname = "_int8multirange",
                Oid = 6157,
            },
            new PgType {
                Typname = "_pg_attrdef",
                Oid = 10000,
            },
            new PgType {
                Typname = "pg_attrdef",
                Oid = 10001,
            },
            new PgType {
                Typname = "_pg_constraint",
                Oid = 10002,
            },
            new PgType {
                Typname = "pg_constraint",
                Oid = 10003,
            },
            new PgType {
                Typname = "_pg_inherits",
                Oid = 10004,
            },
            new PgType {
                Typname = "pg_inherits",
                Oid = 10005,
            },
            new PgType {
                Typname = "_pg_index",
                Oid = 10006,
            },
            new PgType {
                Typname = "pg_index",
                Oid = 10007,
            },
            new PgType {
                Typname = "_pg_operator",
                Oid = 10008,
            },
            new PgType {
                Typname = "pg_operator",
                Oid = 10009,
            },
            new PgType {
                Typname = "_pg_opfamily",
                Oid = 10010,
            },
            new PgType {
                Typname = "pg_opfamily",
                Oid = 10011,
            },
            new PgType {
                Typname = "_pg_opclass",
                Oid = 10012,
            },
            new PgType {
                Typname = "pg_opclass",
                Oid = 10013,
            },
            new PgType {
                Typname = "_pg_am",
                Oid = 10014,
            },
            new PgType {
                Typname = "pg_am",
                Oid = 10015,
            },
            new PgType {
                Typname = "_pg_amop",
                Oid = 10016,
            },
            new PgType {
                Typname = "pg_amop",
                Oid = 10017,
            },
            new PgType {
                Typname = "_pg_amproc",
                Oid = 10018,
            },
            new PgType {
                Typname = "pg_amproc",
                Oid = 10019,
            },
            new PgType {
                Typname = "_pg_language",
                Oid = 10020,
            },
            new PgType {
                Typname = "pg_language",
                Oid = 10021,
            },
            new PgType {
                Typname = "_pg_largeobject_metadata",
                Oid = 10022,
            },
            new PgType {
                Typname = "pg_largeobject_metadata",
                Oid = 10023,
            },
            new PgType {
                Typname = "_pg_largeobject",
                Oid = 10024,
            },
            new PgType {
                Typname = "pg_largeobject",
                Oid = 10025,
            },
            new PgType {
                Typname = "_pg_aggregate",
                Oid = 10026,
            },
            new PgType {
                Typname = "pg_aggregate",
                Oid = 10027,
            },
            new PgType {
                Typname = "_pg_statistic",
                Oid = 10028,
            },
            new PgType {
                Typname = "pg_statistic",
                Oid = 10029,
            },
            new PgType {
                Typname = "_pg_statistic_ext",
                Oid = 10030,
            },
            new PgType {
                Typname = "pg_statistic_ext",
                Oid = 10031,
            },
            new PgType {
                Typname = "_pg_statistic_ext_data",
                Oid = 10032,
            },
            new PgType {
                Typname = "pg_statistic_ext_data",
                Oid = 10033,
            },
            new PgType {
                Typname = "_pg_rewrite",
                Oid = 10034,
            },
            new PgType {
                Typname = "pg_rewrite",
                Oid = 10035,
            },
            new PgType {
                Typname = "_pg_trigger",
                Oid = 10036,
            },
            new PgType {
                Typname = "pg_trigger",
                Oid = 10037,
            },
            new PgType {
                Typname = "_pg_event_trigger",
                Oid = 10038,
            },
            new PgType {
                Typname = "pg_event_trigger",
                Oid = 10039,
            },
            new PgType {
                Typname = "_pg_description",
                Oid = 10040,
            },
            new PgType {
                Typname = "pg_description",
                Oid = 10041,
            },
            new PgType {
                Typname = "_pg_cast",
                Oid = 10042,
            },
            new PgType {
                Typname = "pg_cast",
                Oid = 10043,
            },
            new PgType {
                Typname = "_pg_enum",
                Oid = 10044,
            },
            new PgType {
                Typname = "pg_enum",
                Oid = 10045,
            },
            new PgType {
                Typname = "_pg_namespace",
                Oid = 10046,
            },
            new PgType {
                Typname = "pg_namespace",
                Oid = 10047,
            },
            new PgType {
                Typname = "_pg_conversion",
                Oid = 10048,
            },
            new PgType {
                Typname = "pg_conversion",
                Oid = 10049,
            },
            new PgType {
                Typname = "_pg_depend",
                Oid = 10050,
            },
            new PgType {
                Typname = "pg_depend",
                Oid = 10051,
            },
            new PgType {
                Typname = "_pg_database",
                Oid = 10052,
            },
            new PgType {
                Typname = "_pg_db_role_setting",
                Oid = 10053,
            },
            new PgType {
                Typname = "pg_db_role_setting",
                Oid = 10054,
            },
            new PgType {
                Typname = "_pg_tablespace",
                Oid = 10055,
            },
            new PgType {
                Typname = "pg_tablespace",
                Oid = 10056,
            },
            new PgType {
                Typname = "_pg_authid",
                Oid = 10057,
            },
            new PgType {
                Typname = "_pg_auth_members",
                Oid = 10058,
            },
            new PgType {
                Typname = "_pg_shdepend",
                Oid = 10059,
            },
            new PgType {
                Typname = "pg_shdepend",
                Oid = 10060,
            },
            new PgType {
                Typname = "_pg_shdescription",
                Oid = 10061,
            },
            new PgType {
                Typname = "pg_shdescription",
                Oid = 10062,
            },
            new PgType {
                Typname = "_pg_ts_config",
                Oid = 10063,
            },
            new PgType {
                Typname = "pg_ts_config",
                Oid = 10064,
            },
            new PgType {
                Typname = "_pg_ts_config_map",
                Oid = 10065,
            },
            new PgType {
                Typname = "pg_ts_config_map",
                Oid = 10066,
            },
            new PgType {
                Typname = "_pg_ts_dict",
                Oid = 10067,
            },
            new PgType {
                Typname = "pg_ts_dict",
                Oid = 10068,
            },
            new PgType {
                Typname = "_pg_ts_parser",
                Oid = 10069,
            },
            new PgType {
                Typname = "pg_ts_parser",
                Oid = 10070,
            },
            new PgType {
                Typname = "_pg_ts_template",
                Oid = 10071,
            },
            new PgType {
                Typname = "pg_ts_template",
                Oid = 10072,
            },
            new PgType {
                Typname = "_pg_extension",
                Oid = 10073,
            },
            new PgType {
                Typname = "pg_extension",
                Oid = 10074,
            },
            new PgType {
                Typname = "_pg_foreign_data_wrapper",
                Oid = 10075,
            },
            new PgType {
                Typname = "pg_foreign_data_wrapper",
                Oid = 10076,
            },
            new PgType {
                Typname = "_pg_foreign_server",
                Oid = 10077,
            },
            new PgType {
                Typname = "pg_foreign_server",
                Oid = 10078,
            },
            new PgType {
                Typname = "_pg_user_mapping",
                Oid = 10079,
            },
            new PgType {
                Typname = "pg_user_mapping",
                Oid = 10080,
            },
            new PgType {
                Typname = "_pg_foreign_table",
                Oid = 10081,
            },
            new PgType {
                Typname = "pg_foreign_table",
                Oid = 10082,
            },
            new PgType {
                Typname = "_pg_policy",
                Oid = 10083,
            },
            new PgType {
                Typname = "pg_policy",
                Oid = 10084,
            },
            new PgType {
                Typname = "_pg_replication_origin",
                Oid = 10085,
            },
            new PgType {
                Typname = "pg_replication_origin",
                Oid = 10086,
            },
            new PgType {
                Typname = "_pg_default_acl",
                Oid = 10087,
            },
            new PgType {
                Typname = "pg_default_acl",
                Oid = 10088,
            },
            new PgType {
                Typname = "_pg_init_privs",
                Oid = 10089,
            },
            new PgType {
                Typname = "pg_init_privs",
                Oid = 10090,
            },
            new PgType {
                Typname = "_pg_seclabel",
                Oid = 10091,
            },
            new PgType {
                Typname = "pg_seclabel",
                Oid = 10092,
            },
            new PgType {
                Typname = "_pg_shseclabel",
                Oid = 10093,
            },
            new PgType {
                Typname = "_pg_collation",
                Oid = 10094,
            },
            new PgType {
                Typname = "pg_collation",
                Oid = 10095,
            },
            new PgType {
                Typname = "_pg_parameter_acl",
                Oid = 10096,
            },
            new PgType {
                Typname = "pg_parameter_acl",
                Oid = 10097,
            },
            new PgType {
                Typname = "_pg_partitioned_table",
                Oid = 10098,
            },
            new PgType {
                Typname = "pg_partitioned_table",
                Oid = 10099,
            },
            new PgType {
                Typname = "_pg_range",
                Oid = 10100,
            },
            new PgType {
                Typname = "pg_range",
                Oid = 10101,
            },
            new PgType {
                Typname = "_pg_transform",
                Oid = 10102,
            },
            new PgType {
                Typname = "pg_transform",
                Oid = 10103,
            },
            new PgType {
                Typname = "_pg_sequence",
                Oid = 10104,
            },
            new PgType {
                Typname = "pg_sequence",
                Oid = 10105,
            },
            new PgType {
                Typname = "_pg_publication",
                Oid = 10106,
            },
            new PgType {
                Typname = "pg_publication",
                Oid = 10107,
            },
            new PgType {
                Typname = "_pg_publication_namespace",
                Oid = 10108,
            },
            new PgType {
                Typname = "pg_publication_namespace",
                Oid = 10109,
            },
            new PgType {
                Typname = "_pg_publication_rel",
                Oid = 10110,
            },
            new PgType {
                Typname = "pg_publication_rel",
                Oid = 10111,
            },
            new PgType {
                Typname = "_pg_subscription",
                Oid = 10112,
            },
            new PgType {
                Typname = "_pg_subscription_rel",
                Oid = 10113,
            },
            new PgType {
                Typname = "pg_subscription_rel",
                Oid = 10114,
            },
            new PgType {
                Typname = "_pg_roles",
                Oid = 12001,
            },
            new PgType {
                Typname = "pg_roles",
                Oid = 12002,
            },
            new PgType {
                Typname = "_pg_shadow",
                Oid = 12006,
            },
            new PgType {
                Typname = "pg_shadow",
                Oid = 12007,
            },
            new PgType {
                Typname = "_pg_group",
                Oid = 12011,
            },
            new PgType {
                Typname = "pg_group",
                Oid = 12012,
            },
            new PgType {
                Typname = "_pg_user",
                Oid = 12015,
            },
            new PgType {
                Typname = "pg_user",
                Oid = 12016,
            },
            new PgType {
                Typname = "_pg_policies",
                Oid = 12019,
            },
            new PgType {
                Typname = "pg_policies",
                Oid = 12020,
            },
            new PgType {
                Typname = "_pg_rules",
                Oid = 12024,
            },
            new PgType {
                Typname = "pg_rules",
                Oid = 12025,
            },
            new PgType {
                Typname = "_pg_views",
                Oid = 12029,
            },
            new PgType {
                Typname = "pg_views",
                Oid = 12030,
            },
            new PgType {
                Typname = "_pg_tables",
                Oid = 12034,
            },
            new PgType {
                Typname = "pg_tables",
                Oid = 12035,
            },
            new PgType {
                Typname = "_pg_matviews",
                Oid = 12039,
            },
            new PgType {
                Typname = "pg_matviews",
                Oid = 12040,
            },
            new PgType {
                Typname = "_pg_indexes",
                Oid = 12044,
            },
            new PgType {
                Typname = "pg_indexes",
                Oid = 12045,
            },
            new PgType {
                Typname = "_pg_sequences",
                Oid = 12049,
            },
            new PgType {
                Typname = "pg_sequences",
                Oid = 12050,
            },
            new PgType {
                Typname = "_pg_stats",
                Oid = 12054,
            },
            new PgType {
                Typname = "pg_stats",
                Oid = 12055,
            },
            new PgType {
                Typname = "_pg_stats_ext",
                Oid = 12059,
            },
            new PgType {
                Typname = "pg_stats_ext",
                Oid = 12060,
            },
            new PgType {
                Typname = "_pg_stats_ext_exprs",
                Oid = 12064,
            },
            new PgType {
                Typname = "pg_stats_ext_exprs",
                Oid = 12065,
            },
            new PgType {
                Typname = "_pg_publication_tables",
                Oid = 12069,
            },
            new PgType {
                Typname = "pg_publication_tables",
                Oid = 12070,
            },
            new PgType {
                Typname = "_pg_locks",
                Oid = 12074,
            },
            new PgType {
                Typname = "pg_locks",
                Oid = 12075,
            },
            new PgType {
                Typname = "_pg_cursors",
                Oid = 12078,
            },
            new PgType {
                Typname = "pg_cursors",
                Oid = 12079,
            },
            new PgType {
                Typname = "_pg_available_extensions",
                Oid = 12082,
            },
            new PgType {
                Typname = "pg_available_extensions",
                Oid = 12083,
            },
            new PgType {
                Typname = "_pg_available_extension_versions",
                Oid = 12086,
            },
            new PgType {
                Typname = "pg_available_extension_versions",
                Oid = 12087,
            },
            new PgType {
                Typname = "_pg_prepared_xacts",
                Oid = 12091,
            },
            new PgType {
                Typname = "pg_prepared_xacts",
                Oid = 12092,
            },
            new PgType {
                Typname = "_pg_prepared_statements",
                Oid = 12096,
            },
            new PgType {
                Typname = "pg_prepared_statements",
                Oid = 12097,
            },
            new PgType {
                Typname = "_pg_seclabels",
                Oid = 12100,
            },
            new PgType {
                Typname = "pg_seclabels",
                Oid = 12101,
            },
            new PgType {
                Typname = "_pg_settings",
                Oid = 12105,
            },
            new PgType {
                Typname = "pg_settings",
                Oid = 12106,
            },
            new PgType {
                Typname = "_pg_file_settings",
                Oid = 12111,
            },
            new PgType {
                Typname = "pg_file_settings",
                Oid = 12112,
            },
            new PgType {
                Typname = "_pg_hba_file_rules",
                Oid = 12115,
            },
            new PgType {
                Typname = "pg_hba_file_rules",
                Oid = 12116,
            },
            new PgType {
                Typname = "_pg_ident_file_mappings",
                Oid = 12119,
            },
            new PgType {
                Typname = "pg_ident_file_mappings",
                Oid = 12120,
            },
            new PgType {
                Typname = "_pg_timezone_abbrevs",
                Oid = 12123,
            },
            new PgType {
                Typname = "pg_timezone_abbrevs",
                Oid = 12124,
            },
            new PgType {
                Typname = "_pg_timezone_names",
                Oid = 12127,
            },
            new PgType {
                Typname = "pg_timezone_names",
                Oid = 12128,
            },
            new PgType {
                Typname = "_pg_config",
                Oid = 12131,
            },
            new PgType {
                Typname = "pg_config",
                Oid = 12132,
            },
            new PgType {
                Typname = "_pg_shmem_allocations",
                Oid = 12135,
            },
            new PgType {
                Typname = "pg_shmem_allocations",
                Oid = 12136,
            },
            new PgType {
                Typname = "_pg_backend_memory_contexts",
                Oid = 12139,
            },
            new PgType {
                Typname = "pg_backend_memory_contexts",
                Oid = 12140,
            },
            new PgType {
                Typname = "_pg_stat_all_tables",
                Oid = 12143,
            },
            new PgType {
                Typname = "pg_stat_all_tables",
                Oid = 12144,
            },
            new PgType {
                Typname = "_pg_stat_xact_all_tables",
                Oid = 12148,
            },
            new PgType {
                Typname = "pg_stat_xact_all_tables",
                Oid = 12149,
            },
            new PgType {
                Typname = "_pg_stat_sys_tables",
                Oid = 12153,
            },
            new PgType {
                Typname = "pg_stat_sys_tables",
                Oid = 12154,
            },
            new PgType {
                Typname = "_pg_stat_xact_sys_tables",
                Oid = 12158,
            },
            new PgType {
                Typname = "pg_stat_xact_sys_tables",
                Oid = 12159,
            },
            new PgType {
                Typname = "_pg_stat_user_tables",
                Oid = 12162,
            },
            new PgType {
                Typname = "pg_stat_user_tables",
                Oid = 12163,
            },
            new PgType {
                Typname = "_pg_stat_xact_user_tables",
                Oid = 12167,
            },
            new PgType {
                Typname = "pg_stat_xact_user_tables",
                Oid = 12168,
            },
            new PgType {
                Typname = "_pg_statio_all_tables",
                Oid = 12171,
            },
            new PgType {
                Typname = "pg_statio_all_tables",
                Oid = 12172,
            },
            new PgType {
                Typname = "_pg_statio_sys_tables",
                Oid = 12176,
            },
            new PgType {
                Typname = "pg_statio_sys_tables",
                Oid = 12177,
            },
            new PgType {
                Typname = "_pg_statio_user_tables",
                Oid = 12180,
            },
            new PgType {
                Typname = "pg_statio_user_tables",
                Oid = 12181,
            },
            new PgType {
                Typname = "_pg_stat_all_indexes",
                Oid = 12184,
            },
            new PgType {
                Typname = "pg_stat_all_indexes",
                Oid = 12185,
            },
            new PgType {
                Typname = "_pg_stat_sys_indexes",
                Oid = 12189,
            },
            new PgType {
                Typname = "pg_stat_sys_indexes",
                Oid = 12190,
            },
            new PgType {
                Typname = "_pg_stat_user_indexes",
                Oid = 12193,
            },
            new PgType {
                Typname = "pg_stat_user_indexes",
                Oid = 12194,
            },
            new PgType {
                Typname = "_pg_statio_all_indexes",
                Oid = 12197,
            },
            new PgType {
                Typname = "pg_statio_all_indexes",
                Oid = 12198,
            },
            new PgType {
                Typname = "_pg_statio_sys_indexes",
                Oid = 12202,
            },
            new PgType {
                Typname = "pg_statio_sys_indexes",
                Oid = 12203,
            },
            new PgType {
                Typname = "_pg_statio_user_indexes",
                Oid = 12206,
            },
            new PgType {
                Typname = "pg_statio_user_indexes",
                Oid = 12207,
            },
            new PgType {
                Typname = "_pg_statio_all_sequences",
                Oid = 12210,
            },
            new PgType {
                Typname = "pg_statio_all_sequences",
                Oid = 12211,
            },
            new PgType {
                Typname = "_pg_statio_sys_sequences",
                Oid = 12215,
            },
            new PgType {
                Typname = "pg_statio_sys_sequences",
                Oid = 12216,
            },
            new PgType {
                Typname = "_pg_statio_user_sequences",
                Oid = 12219,
            },
            new PgType {
                Typname = "pg_statio_user_sequences",
                Oid = 12220,
            },
            new PgType {
                Typname = "_pg_stat_activity",
                Oid = 12223,
            },
            new PgType {
                Typname = "pg_stat_activity",
                Oid = 12224,
            },
            new PgType {
                Typname = "_pg_stat_replication",
                Oid = 12228,
            },
            new PgType {
                Typname = "pg_stat_replication",
                Oid = 12229,
            },
            new PgType {
                Typname = "_pg_stat_slru",
                Oid = 12233,
            },
            new PgType {
                Typname = "pg_stat_slru",
                Oid = 12234,
            },
            new PgType {
                Typname = "_pg_stat_wal_receiver",
                Oid = 12237,
            },
            new PgType {
                Typname = "pg_stat_wal_receiver",
                Oid = 12238,
            },
            new PgType {
                Typname = "_pg_stat_recovery_prefetch",
                Oid = 12241,
            },
            new PgType {
                Typname = "pg_stat_recovery_prefetch",
                Oid = 12242,
            },
            new PgType {
                Typname = "_pg_stat_subscription",
                Oid = 12245,
            },
            new PgType {
                Typname = "pg_stat_subscription",
                Oid = 12246,
            },
            new PgType {
                Typname = "_pg_stat_ssl",
                Oid = 12250,
            },
            new PgType {
                Typname = "pg_stat_ssl",
                Oid = 12251,
            },
            new PgType {
                Typname = "_pg_stat_gssapi",
                Oid = 12254,
            },
            new PgType {
                Typname = "pg_stat_gssapi",
                Oid = 12255,
            },
            new PgType {
                Typname = "_pg_replication_slots",
                Oid = 12258,
            },
            new PgType {
                Typname = "pg_replication_slots",
                Oid = 12259,
            },
            new PgType {
                Typname = "_pg_stat_replication_slots",
                Oid = 12263,
            },
            new PgType {
                Typname = "pg_stat_replication_slots",
                Oid = 12264,
            },
            new PgType {
                Typname = "_pg_stat_database",
                Oid = 12267,
            },
            new PgType {
                Typname = "pg_stat_database",
                Oid = 12268,
            },
            new PgType {
                Typname = "_pg_stat_database_conflicts",
                Oid = 12272,
            },
            new PgType {
                Typname = "pg_stat_database_conflicts",
                Oid = 12273,
            },
            new PgType {
                Typname = "_pg_stat_user_functions",
                Oid = 12276,
            },
            new PgType {
                Typname = "pg_stat_user_functions",
                Oid = 12277,
            },
            new PgType {
                Typname = "_pg_stat_xact_user_functions",
                Oid = 12281,
            },
            new PgType {
                Typname = "pg_stat_xact_user_functions",
                Oid = 12282,
            },
            new PgType {
                Typname = "_pg_stat_archiver",
                Oid = 12286,
            },
            new PgType {
                Typname = "pg_stat_archiver",
                Oid = 12287,
            },
            new PgType {
                Typname = "_pg_stat_bgwriter",
                Oid = 12290,
            },
            new PgType {
                Typname = "pg_stat_bgwriter",
                Oid = 12291,
            },
            new PgType {
                Typname = "_pg_stat_wal",
                Oid = 12294,
            },
            new PgType {
                Typname = "pg_stat_wal",
                Oid = 12295,
            },
            new PgType {
                Typname = "_pg_stat_progress_analyze",
                Oid = 12298,
            },
            new PgType {
                Typname = "pg_stat_progress_analyze",
                Oid = 12299,
            },
            new PgType {
                Typname = "_pg_stat_progress_vacuum",
                Oid = 12303,
            },
            new PgType {
                Typname = "pg_stat_progress_vacuum",
                Oid = 12304,
            },
            new PgType {
                Typname = "_pg_stat_progress_cluster",
                Oid = 12308,
            },
            new PgType {
                Typname = "pg_stat_progress_cluster",
                Oid = 12309,
            },
            new PgType {
                Typname = "_pg_stat_progress_create_index",
                Oid = 12313,
            },
            new PgType {
                Typname = "pg_stat_progress_create_index",
                Oid = 12314,
            },
            new PgType {
                Typname = "_pg_stat_progress_basebackup",
                Oid = 12318,
            },
            new PgType {
                Typname = "pg_stat_progress_basebackup",
                Oid = 12319,
            },
            new PgType {
                Typname = "_pg_stat_progress_copy",
                Oid = 12323,
            },
            new PgType {
                Typname = "pg_stat_progress_copy",
                Oid = 12324,
            },
            new PgType {
                Typname = "_pg_user_mappings",
                Oid = 12328,
            },
            new PgType {
                Typname = "pg_user_mappings",
                Oid = 12329,
            },
            new PgType {
                Typname = "_pg_replication_origin_status",
                Oid = 12333,
            },
            new PgType {
                Typname = "pg_replication_origin_status",
                Oid = 12334,
            },
            new PgType {
                Typname = "_pg_stat_subscription_stats",
                Oid = 12337,
            },
            new PgType {
                Typname = "pg_stat_subscription_stats",
                Oid = 12338,
            },
            new PgType {
                Typname = "_cardinal_number",
                Oid = 12416,
            },
            new PgType {
                Typname = "cardinal_number",
                Oid = 12417,
            },
            new PgType {
                Typname = "_character_data",
                Oid = 12419,
            },
            new PgType {
                Typname = "character_data",
                Oid = 12420,
            },
            new PgType {
                Typname = "_sql_identifier",
                Oid = 12421,
            },
            new PgType {
                Typname = "sql_identifier",
                Oid = 12422,
            },
            new PgType {
                Typname = "_information_schema_catalog_name",
                Oid = 12424,
            },
            new PgType {
                Typname = "information_schema_catalog_name",
                Oid = 12425,
            },
            new PgType {
                Typname = "_time_stamp",
                Oid = 12427,
            },
            new PgType {
                Typname = "time_stamp",
                Oid = 12428,
            },
            new PgType {
                Typname = "_yes_or_no",
                Oid = 12429,
            },
            new PgType {
                Typname = "yes_or_no",
                Oid = 12430,
            },
            new PgType {
                Typname = "_applicable_roles",
                Oid = 12433,
            },
            new PgType {
                Typname = "applicable_roles",
                Oid = 12434,
            },
            new PgType {
                Typname = "_administrable_role_authorizations",
                Oid = 12438,
            },
            new PgType {
                Typname = "administrable_role_authorizations",
                Oid = 12439,
            },
            new PgType {
                Typname = "_attributes",
                Oid = 12442,
            },
            new PgType {
                Typname = "attributes",
                Oid = 12443,
            },
            new PgType {
                Typname = "_character_sets",
                Oid = 12447,
            },
            new PgType {
                Typname = "character_sets",
                Oid = 12448,
            },
            new PgType {
                Typname = "_check_constraint_routine_usage",
                Oid = 12452,
            },
            new PgType {
                Typname = "check_constraint_routine_usage",
                Oid = 12453,
            },
            new PgType {
                Typname = "_check_constraints",
                Oid = 12457,
            },
            new PgType {
                Typname = "check_constraints",
                Oid = 12458,
            },
            new PgType {
                Typname = "_collations",
                Oid = 12462,
            },
            new PgType {
                Typname = "collations",
                Oid = 12463,
            },
            new PgType {
                Typname = "_collation_character_set_applicability",
                Oid = 12467,
            },
            new PgType {
                Typname = "collation_character_set_applicability",
                Oid = 12468,
            },
            new PgType {
                Typname = "_column_column_usage",
                Oid = 12472,
            },
            new PgType {
                Typname = "column_column_usage",
                Oid = 12473,
            },
            new PgType {
                Typname = "_column_domain_usage",
                Oid = 12477,
            },
            new PgType {
                Typname = "column_domain_usage",
                Oid = 12478,
            },
            new PgType {
                Typname = "_column_privileges",
                Oid = 12482,
            },
            new PgType {
                Typname = "column_privileges",
                Oid = 12483,
            },
            new PgType {
                Typname = "_column_udt_usage",
                Oid = 12487,
            },
            new PgType {
                Typname = "column_udt_usage",
                Oid = 12488,
            },
            new PgType {
                Typname = "_columns",
                Oid = 12492,
            },
            new PgType {
                Typname = "columns",
                Oid = 12493,
            },
            new PgType {
                Typname = "_constraint_column_usage",
                Oid = 12497,
            },
            new PgType {
                Typname = "constraint_column_usage",
                Oid = 12498,
            },
            new PgType {
                Typname = "_constraint_table_usage",
                Oid = 12502,
            },
            new PgType {
                Typname = "constraint_table_usage",
                Oid = 12503,
            },
            new PgType {
                Typname = "_domain_constraints",
                Oid = 12507,
            },
            new PgType {
                Typname = "domain_constraints",
                Oid = 12508,
            },
            new PgType {
                Typname = "_domain_udt_usage",
                Oid = 12512,
            },
            new PgType {
                Typname = "domain_udt_usage",
                Oid = 12513,
            },
            new PgType {
                Typname = "_domains",
                Oid = 12517,
            },
            new PgType {
                Typname = "domains",
                Oid = 12518,
            },
            new PgType {
                Typname = "_enabled_roles",
                Oid = 12522,
            },
            new PgType {
                Typname = "enabled_roles",
                Oid = 12523,
            },
            new PgType {
                Typname = "_key_column_usage",
                Oid = 12526,
            },
            new PgType {
                Typname = "key_column_usage",
                Oid = 12527,
            },
            new PgType {
                Typname = "_parameters",
                Oid = 12531,
            },
            new PgType {
                Typname = "parameters",
                Oid = 12532,
            },
            new PgType {
                Typname = "_referential_constraints",
                Oid = 12536,
            },
            new PgType {
                Typname = "referential_constraints",
                Oid = 12537,
            },
            new PgType {
                Typname = "_role_column_grants",
                Oid = 12541,
            },
            new PgType {
                Typname = "role_column_grants",
                Oid = 12542,
            },
            new PgType {
                Typname = "_routine_column_usage",
                Oid = 12545,
            },
            new PgType {
                Typname = "routine_column_usage",
                Oid = 12546,
            },
            new PgType {
                Typname = "_routine_privileges",
                Oid = 12550,
            },
            new PgType {
                Typname = "routine_privileges",
                Oid = 12551,
            },
            new PgType {
                Typname = "_role_routine_grants",
                Oid = 12555,
            },
            new PgType {
                Typname = "role_routine_grants",
                Oid = 12556,
            },
            new PgType {
                Typname = "_routine_routine_usage",
                Oid = 12559,
            },
            new PgType {
                Typname = "routine_routine_usage",
                Oid = 12560,
            },
            new PgType {
                Typname = "_routine_sequence_usage",
                Oid = 12564,
            },
            new PgType {
                Typname = "routine_sequence_usage",
                Oid = 12565,
            },
            new PgType {
                Typname = "_routine_table_usage",
                Oid = 12569,
            },
            new PgType {
                Typname = "routine_table_usage",
                Oid = 12570,
            },
            new PgType {
                Typname = "_routines",
                Oid = 12574,
            },
            new PgType {
                Typname = "routines",
                Oid = 12575,
            },
            new PgType {
                Typname = "_schemata",
                Oid = 12579,
            },
            new PgType {
                Typname = "schemata",
                Oid = 12580,
            },
            new PgType {
                Typname = "_sequences",
                Oid = 12583,
            },
            new PgType {
                Typname = "sequences",
                Oid = 12584,
            },
            new PgType {
                Typname = "_sql_features",
                Oid = 12588,
            },
            new PgType {
                Typname = "sql_features",
                Oid = 12589,
            },
            new PgType {
                Typname = "_sql_implementation_info",
                Oid = 12593,
            },
            new PgType {
                Typname = "sql_implementation_info",
                Oid = 12594,
            },
            new PgType {
                Typname = "_sql_parts",
                Oid = 12598,
            },
            new PgType {
                Typname = "sql_parts",
                Oid = 12599,
            },
            new PgType {
                Typname = "_sql_sizing",
                Oid = 12603,
            },
            new PgType {
                Typname = "sql_sizing",
                Oid = 12604,
            },
            new PgType {
                Typname = "_table_constraints",
                Oid = 12608,
            },
            new PgType {
                Typname = "table_constraints",
                Oid = 12609,
            },
            new PgType {
                Typname = "_table_privileges",
                Oid = 12613,
            },
            new PgType {
                Typname = "table_privileges",
                Oid = 12614,
            },
            new PgType {
                Typname = "_role_table_grants",
                Oid = 12618,
            },
            new PgType {
                Typname = "role_table_grants",
                Oid = 12619,
            },
            new PgType {
                Typname = "_tables",
                Oid = 12622,
            },
            new PgType {
                Typname = "tables",
                Oid = 12623,
            },
            new PgType {
                Typname = "_transforms",
                Oid = 12627,
            },
            new PgType {
                Typname = "transforms",
                Oid = 12628,
            },
            new PgType {
                Typname = "_triggered_update_columns",
                Oid = 12632,
            },
            new PgType {
                Typname = "triggered_update_columns",
                Oid = 12633,
            },
            new PgType {
                Typname = "_triggers",
                Oid = 12637,
            },
            new PgType {
                Typname = "triggers",
                Oid = 12638,
            },
            new PgType {
                Typname = "_udt_privileges",
                Oid = 12642,
            },
            new PgType {
                Typname = "udt_privileges",
                Oid = 12643,
            },
            new PgType {
                Typname = "_role_udt_grants",
                Oid = 12647,
            },
            new PgType {
                Typname = "role_udt_grants",
                Oid = 12648,
            },
            new PgType {
                Typname = "_usage_privileges",
                Oid = 12651,
            },
            new PgType {
                Typname = "usage_privileges",
                Oid = 12652,
            },
            new PgType {
                Typname = "_role_usage_grants",
                Oid = 12656,
            },
            new PgType {
                Typname = "role_usage_grants",
                Oid = 12657,
            },
            new PgType {
                Typname = "_user_defined_types",
                Oid = 12660,
            },
            new PgType {
                Typname = "user_defined_types",
                Oid = 12661,
            },
            new PgType {
                Typname = "_view_column_usage",
                Oid = 12665,
            },
            new PgType {
                Typname = "view_column_usage",
                Oid = 12666,
            },
            new PgType {
                Typname = "_view_routine_usage",
                Oid = 12670,
            },
            new PgType {
                Typname = "view_routine_usage",
                Oid = 12671,
            },
            new PgType {
                Typname = "_view_table_usage",
                Oid = 12675,
            },
            new PgType {
                Typname = "view_table_usage",
                Oid = 12676,
            },
            new PgType {
                Typname = "_views",
                Oid = 12680,
            },
            new PgType {
                Typname = "views",
                Oid = 12681,
            },
            new PgType {
                Typname = "_data_type_privileges",
                Oid = 12685,
            },
            new PgType {
                Typname = "data_type_privileges",
                Oid = 12686,
            },
            new PgType {
                Typname = "_element_types",
                Oid = 12690,
            },
            new PgType {
                Typname = "element_types",
                Oid = 12691,
            },
            new PgType {
                Typname = "__pg_foreign_table_columns",
                Oid = 12695,
            },
            new PgType {
                Typname = "_pg_foreign_table_columns",
                Oid = 12696,
            },
            new PgType {
                Typname = "_column_options",
                Oid = 12700,
            },
            new PgType {
                Typname = "column_options",
                Oid = 12701,
            },
            new PgType {
                Typname = "__pg_foreign_data_wrappers",
                Oid = 12704,
            },
            new PgType {
                Typname = "_pg_foreign_data_wrappers",
                Oid = 12705,
            },
            new PgType {
                Typname = "_foreign_data_wrapper_options",
                Oid = 12708,
            },
            new PgType {
                Typname = "foreign_data_wrapper_options",
                Oid = 12709,
            },
            new PgType {
                Typname = "_foreign_data_wrappers",
                Oid = 12712,
            },
            new PgType {
                Typname = "foreign_data_wrappers",
                Oid = 12713,
            },
            new PgType {
                Typname = "__pg_foreign_servers",
                Oid = 12716,
            },
            new PgType {
                Typname = "_pg_foreign_servers",
                Oid = 12717,
            },
            new PgType {
                Typname = "_foreign_server_options",
                Oid = 12721,
            },
            new PgType {
                Typname = "foreign_server_options",
                Oid = 12722,
            },
            new PgType {
                Typname = "_foreign_servers",
                Oid = 12725,
            },
            new PgType {
                Typname = "foreign_servers",
                Oid = 12726,
            },
            new PgType {
                Typname = "__pg_foreign_tables",
                Oid = 12729,
            },
            new PgType {
                Typname = "_pg_foreign_tables",
                Oid = 12730,
            },
            new PgType {
                Typname = "_foreign_table_options",
                Oid = 12734,
            },
            new PgType {
                Typname = "foreign_table_options",
                Oid = 12735,
            },
            new PgType {
                Typname = "_foreign_tables",
                Oid = 12738,
            },
            new PgType {
                Typname = "foreign_tables",
                Oid = 12739,
            },
            new PgType {
                Typname = "__pg_user_mappings",
                Oid = 12742,
            },
            new PgType {
                Typname = "_pg_user_mappings",
                Oid = 12743,
            },
            new PgType {
                Typname = "_user_mapping_options",
                Oid = 12747,
            },
            new PgType {
                Typname = "user_mapping_options",
                Oid = 12748,
            },
            new PgType {
                Typname = "_user_mappings",
                Oid = 12752,
            },
            new PgType {
                Typname = "user_mappings",
                Oid = 12753,
            },
            new PgType {
                Typname = "_crypto_box_keypair",
                Oid = 16657,
            },
            new PgType {
                Typname = "crypto_box_keypair",
                Oid = 16658,
            },
            new PgType {
                Typname = "_crypto_sign_keypair",
                Oid = 16664,
            },
            new PgType {
                Typname = "crypto_sign_keypair",
                Oid = 16665,
            },
            new PgType {
                Typname = "_crypto_kx_keypair",
                Oid = 16680,
            },
            new PgType {
                Typname = "crypto_kx_keypair",
                Oid = 16681,
            },
            new PgType {
                Typname = "_crypto_kx_session",
                Oid = 16686,
            },
            new PgType {
                Typname = "crypto_kx_session",
                Oid = 16687,
            },
            new PgType {
                Typname = "_crypto_signcrypt_state_key",
                Oid = 16754,
            },
            new PgType {
                Typname = "crypto_signcrypt_state_key",
                Oid = 16755,
            },
            new PgType {
                Typname = "_crypto_signcrypt_keypair",
                Oid = 16757,
            },
            new PgType {
                Typname = "crypto_signcrypt_keypair",
                Oid = 16758,
            },
            new PgType {
                Typname = "_key_status",
                Oid = 16771,
            },
            new PgType {
                Typname = "key_status",
                Oid = 16772,
            },
            new PgType {
                Typname = "_key_type",
                Oid = 16781,
            },
            new PgType {
                Typname = "key_type",
                Oid = 16782,
            },
            new PgType {
                Typname = "_key",
                Oid = 16789,
            },
            new PgType {
                Typname = "key",
                Oid = 16790,
            },
            new PgType {
                Typname = "__key_id_context",
                Oid = 16810,
            },
            new PgType {
                Typname = "_key_id_context",
                Oid = 16811,
            },
            new PgType {
                Typname = "_valid_key",
                Oid = 16893,
            },
            new PgType {
                Typname = "valid_key",
                Oid = 16894,
            },
            new PgType {
                Typname = "_masking_rule",
                Oid = 16909,
            },
            new PgType {
                Typname = "masking_rule",
                Oid = 16910,
            },
            new PgType {
                Typname = "_mask_columns",
                Oid = 16914,
            },
            new PgType {
                Typname = "mask_columns",
                Oid = 16915,
            },
            new PgType {
                Typname = "_decrypted_key",
                Oid = 16939,
            },
            new PgType {
                Typname = "decrypted_key",
                Oid = 16940,
            },
            new PgType {
                Typname = "_pg_stat_statements_info",
                Oid = 27035,
            },
            new PgType {
                Typname = "pg_stat_statements_info",
                Oid = 27036,
            },
            new PgType {
                Typname = "_pg_stat_statements",
                Oid = 27046,
            },
            new PgType {
                Typname = "pg_stat_statements",
                Oid = 27047,
            },
            new PgType {
                Typname = "_aal_level",
                Oid = 27105,
            },
            new PgType {
                Typname = "aal_level",
                Oid = 27106,
            },
            new PgType {
                Typname = "_factor_status",
                Oid = 27113,
            },
            new PgType {
                Typname = "factor_status",
                Oid = 27114,
            },
            new PgType {
                Typname = "_factor_type",
                Oid = 27119,
            },
            new PgType {
                Typname = "factor_type",
                Oid = 27120,
            },
            new PgType {
                Typname = "_audit_log_entries",
                Oid = 27142,
            },
            new PgType {
                Typname = "audit_log_entries",
                Oid = 27143,
            },
            new PgType {
                Typname = "_identities",
                Oid = 27148,
            },
            new PgType {
                Typname = "identities",
                Oid = 27149,
            },
            new PgType {
                Typname = "_instances",
                Oid = 27153,
            },
            new PgType {
                Typname = "instances",
                Oid = 27154,
            },
            new PgType {
                Typname = "_mfa_amr_claims",
                Oid = 27158,
            },
            new PgType {
                Typname = "mfa_amr_claims",
                Oid = 27159,
            },
            new PgType {
                Typname = "_mfa_challenges",
                Oid = 27163,
            },
            new PgType {
                Typname = "mfa_challenges",
                Oid = 27164,
            },
            new PgType {
                Typname = "_mfa_factors",
                Oid = 27168,
            },
            new PgType {
                Typname = "mfa_factors",
                Oid = 27169,
            },
            new PgType {
                Typname = "_refresh_tokens",
                Oid = 27173,
            },
            new PgType {
                Typname = "refresh_tokens",
                Oid = 27174,
            },
            new PgType {
                Typname = "_saml_providers",
                Oid = 27179,
            },
            new PgType {
                Typname = "saml_providers",
                Oid = 27180,
            },
            new PgType {
                Typname = "_saml_relay_states",
                Oid = 27187,
            },
            new PgType {
                Typname = "saml_relay_states",
                Oid = 27188,
            },
            new PgType {
                Typname = "_schema_migrations",
                Oid = 27193,
            },
            new PgType {
                Typname = "schema_migrations",
                Oid = 27194,
            },
            new PgType {
                Typname = "_sessions",
                Oid = 27196,
            },
            new PgType {
                Typname = "sessions",
                Oid = 27197,
            },
            new PgType {
                Typname = "_sso_domains",
                Oid = 27199,
            },
            new PgType {
                Typname = "sso_domains",
                Oid = 27200,
            },
            new PgType {
                Typname = "_sso_providers",
                Oid = 27205,
            },
            new PgType {
                Typname = "sso_providers",
                Oid = 27206,
            },
            new PgType {
                Typname = "_users",
                Oid = 27215,
            },
            new PgType {
                Typname = "users",
                Oid = 27216,
            },
            new PgType {
                Typname = "_invoice",
                Oid = 27228,
            },
            new PgType {
                Typname = "invoice",
                Oid = 27229,
            },
            new PgType {
                Typname = "_subscription",
                Oid = 27234,
            },
            new PgType {
                Typname = "subscription",
                Oid = 27235,
            },
            new PgType {
                Typname = "_transactions",
                Oid = 27240,
            },
            new PgType {
                Typname = "transactions",
                Oid = 27241,
            },
            new PgType {
                Typname = "_buckets",
                Oid = 27247,
            },
            new PgType {
                Typname = "buckets",
                Oid = 27248,
            },
            new PgType {
                Typname = "_migrations",
                Oid = 27255,
            },
            new PgType {
                Typname = "migrations",
                Oid = 27256,
            },
            new PgType {
                Typname = "_objects",
                Oid = 27259,
            },
            new PgType {
                Typname = "objects",
                Oid = 27260,
            },
            new PgType {
                Typname = "_code_challenge_method",
                Oid = 27516,
            },
            new PgType {
                Typname = "code_challenge_method",
                Oid = 27517,
            },
            new PgType {
                Typname = "_flow_state",
                Oid = 27522,
            },
            new PgType {
                Typname = "flow_state",
                Oid = 27523,
            },
            new PgType {
                Typname = "_customers",
                Oid = 27531,
            },
            new PgType {
                Typname = "customers",
                Oid = 27532,
            },
        };
    }
}