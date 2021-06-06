using System.Collections.Generic;

namespace holonsoft.Utils.Dto
{
    /// <summary>
    /// Contains static infos for IBANs of several countries
    /// </summary>
    public class IBANCountryInfo
    {
        // just for info purpose
        // datasource: https://de.wikipedia.org/w/index.php?title=IBAN&oldid=147662543
        public static string DataTopicality = "2015-11-01";

        /// <summary>
        /// 2-letter-code of country
        /// </summary>
        public string CountryCode => IBANReadableFormat.Substring(0, 2);

        /// <summary>
        /// English name of country
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// length of IBAN in a country
        /// </summary>
        public int IBANLength => IBANFormat.Length;

        /// <summary>
        /// Legend: POS 1 - 2: Country code
        ///         POS 3 - 4: pp checksum
        ///         b: bank code
        ///         d: account type
        ///         k: account cipher
        ///         K: control cipher
        ///         r: regional code
        ///         s: branch code
        ///         X: other
        /// 
        /// For better reading raw data in list contain spaces. These spaces do not count for length!
        /// </summary>
        public string IBANReadableFormat { get; set; }

        /// <summary>
        /// Needed for checksum calculatation / validation
        /// </summary>
        public int CountryCodeChecksumValue => (((int) CountryCode[0] - 55) * 100) + ((int) CountryCode[1] - 55);

        /// <summary>
        /// Same as IBANReadableFormat but doesn't contain any spaces
        /// </summary>
        public string IBANFormat => IBANReadableFormat.Replace(" ","");

        /// <summary>
        /// A list of supported IBAN countries
        /// </summary>
        public static Dictionary<string, IBANCountryInfo> IBANCountryList = new Dictionary<string, IBANCountryInfo>()
        {
            {
                "EG", new IBANCountryInfo
                {
                    IBANReadableFormat = "EGpp kkkk kkkk kkkk kkkk kkkk kkk",
                    CountryName = "Egypt",
                }
            },

            {
                "AL", new IBANCountryInfo
                {
                    IBANReadableFormat = "ALpp bbbs sssK kkkk kkkk kkkk kkkk",
                    CountryName = "Albania",
                }
            },

            {
                "DZ", new IBANCountryInfo
                {
                    IBANReadableFormat = "DZpp kkkk kkkk kkkk kkkk kkkk",
                    CountryName = "Algeria",
                }
            },

            {
                "AD", new IBANCountryInfo
                {
                    IBANReadableFormat = "ADpp bbbb ssss kkkk kkkk kkkk",
                    CountryName = "Andorra",
                }
            },

            {
                "AO", new IBANCountryInfo
                {
                    IBANReadableFormat = "AOpp bbbb ssss kkkk kkkk kkkK K",
                    CountryName = "Angola",
                }
            },

            {
                "AZ", new IBANCountryInfo
                {
                    IBANReadableFormat = "AZpp bbbb kkkk kkkk kkkk kkkk kkkk",
                    CountryName = "Azerbaijan",
                }
            },

            {
                "BH", new IBANCountryInfo
                {
                    IBANReadableFormat = "BHpp bbbb kkkk kkkk kkkk kk",
                    CountryName = "Bahrein",
                }
            },
            
            {
                "BE", new IBANCountryInfo
                {
                    IBANReadableFormat = "BEpp bbbk kkkk kkKK",
                    CountryName = "Belgium",
                }
            },
            
            {
                "BJ", new IBANCountryInfo
                {
                    IBANReadableFormat = "BJpp bbbb bsss sskk kkkk kkkk kkKK",
                    CountryName = "Benin",
                }
            },
            
            {
                "BA", new IBANCountryInfo
                {
                    IBANReadableFormat = "BApp bbbs sskk kkkk kkKK",
                    CountryName = "Bosnia and Herzegovina",
                }
            },

            {
                "BR", new IBANCountryInfo
                {
                    IBANReadableFormat = "BRpp bbbb bbbb ssss skkk kkkk kkkk k",
                    CountryName = "Brazil",
                }
            },

            {
                "VG", new IBANCountryInfo
                {
                    IBANReadableFormat = "VGpp bbbb kkkk kkkk kkkk kkkk",
                    CountryName = "British Virgin Islands",
                }
            },

            {
                "BG", new IBANCountryInfo
                {
                    IBANReadableFormat = "BGpp bbbb ssss ddkk kkkk kk",
                    CountryName = "Bulgaria",
                }
            },
            
            {
                "BF", new IBANCountryInfo
                {
                    IBANReadableFormat = "BFpp bbbb bsss sskk kkkk kkkk kKK",
                    CountryName = "Burkina Faso",
                }
            },
            
            {
                "BI", new IBANCountryInfo
                {
                    IBANReadableFormat = "BIpp kkkk kkkk kkkk",
                    CountryName = "Burundi",
                }
            },

            {
                "CR", new IBANCountryInfo
                {
                    IBANReadableFormat = "CRpp bbbk kkkk kkkk kkkk k",
                    CountryName = "Costa Rica",
                }
            },

            {
                "CI", new IBANCountryInfo
                {
                    IBANReadableFormat = "CIpp bbbb bsss sskk kkkk kkkk kkKK",
                    CountryName = "Côte d'Ivoire (Ivory Coast)",
                }
            },

            
            {
                "DK", new IBANCountryInfo
                {
                    IBANReadableFormat = "DKpp bbbb kkkk kkkk kK",
                    CountryName = "Danmark",
                }
            },

            {
                "DE", new IBANCountryInfo
                {
                    IBANReadableFormat = "DEpp bbbb bbbb kkkk kkkk kk",
                    CountryName = "Germany",
                }
            },

            {
                "DO", new IBANCountryInfo
                {
                    IBANReadableFormat = "DOpp bbbb kkkk kkkk kkkk kkkk kkkk",
                    CountryName = "Dominican Republic",
                }
            },

            {
                "EE", new IBANCountryInfo
                {
                    IBANReadableFormat = "EEpp bbkk kkkk kkkk kkkK",
                    CountryName = "Estonia",
                }
            },

            {
                "FO", new IBANCountryInfo
                {
                    IBANReadableFormat = "FOpp bbbb kkkk kkkk kK",
                    CountryName = "Faroe",
                }
            },

            {
                "FI", new IBANCountryInfo
                {
                    IBANReadableFormat = "FIpp bbbb bbkk kkkk kK",
                    CountryName = "Finland",
                }
            },

            {
                "FR", new IBANCountryInfo
                {
                    IBANReadableFormat = "FRpp bbbb bsss sskk kkkk kkkk kKK",
                    CountryName = "France (including French Guiana, French Polynesia, French Southern and Antarctic Territories, Guadeloupe,  Martinique, Réunion, Mayotte, New Caledonia, Saint-Barthélemy, Saint-Martin, Saint-Pierre and Miquelon, Wallis and Futuna Islands)",
                }
            },

            {
                "GA", new IBANCountryInfo
                {
                    IBANReadableFormat = "GApp bbbb bsss sskk kkkk kkkk kKK",
                    CountryName = "Gabon",
                }
            },

            {
                "GE", new IBANCountryInfo
                {
                    IBANReadableFormat = "GEpp bbkk kkkk kkkk kkkk kk",
                    CountryName = "Georgia",
                }
            },

            {
                "GI", new IBANCountryInfo
                {
                    IBANReadableFormat = "GIpp bbbb kkkk kkkk kkkk kkk",
                    CountryName = "Gibraltar",
                }
            },

            {
                "GR", new IBANCountryInfo
                {
                    IBANReadableFormat = "GRpp bbbs sssk kkkk kkkk kkkk kkk",
                    CountryName = "Greece",
                }
            },

            {
                "GL", new IBANCountryInfo
                {
                    IBANReadableFormat = "GLpp bbbb kkkk kkkk kK",
                    CountryName = "Greenland",
                }
            },

            {
                "GT", new IBANCountryInfo
                {
                    IBANReadableFormat = "GTpp bbbb kkkk kkkk kkkk kkkk kkkk",
                    CountryName = "Guatemala",
                }
            },

            {
                "IR", new IBANCountryInfo
                {
                    IBANReadableFormat = "IRpp kkkk kkkk kkkk kkkk kkkk kk",
                    CountryName = "Iran",
                }
            },

            {
                "IE", new IBANCountryInfo
                {
                    IBANReadableFormat = "IEpp bbbb ssss sskk kkkk kk",
                    CountryName = "Ireland",
                }
            },

            {
                "IS", new IBANCountryInfo
                {
                    IBANReadableFormat = "ISpp bbbb sskk kkkk XXXX XXXX XX",
                    CountryName = "Iceland",
                }
            },

            {
                "IL", new IBANCountryInfo
                {
                    IBANReadableFormat = "ILpp bbbs sskk kkkk kkkk kkk",
                    CountryName = "Israel",
                }
            },

            {
                "IT", new IBANCountryInfo
                {
                    IBANReadableFormat = "ITpp Kbbb bbss sssk kkkk kkkk kkk",
                    CountryName = "Italy",
                }
            },

            {
                "JO", new IBANCountryInfo
                {
                    IBANReadableFormat = "JOpp bbbb ssss kkkk kkkk kkkk kkkk kk",
                    CountryName = "Jordan",
                }
            },

            {
                "CM", new IBANCountryInfo
                {
                    IBANReadableFormat = "CMpp bbbb bsss sskk kkkk kkkk kKK",
                    CountryName = "Camerun",
                }
            },

            {
                "CV", new IBANCountryInfo
                {
                    IBANReadableFormat = "CVpp bbbb ssss kkkk kkkk kkkK K",
                    CountryName = "Cape Verde",
                }
            },

            {
                "KZ", new IBANCountryInfo
                {
                    IBANReadableFormat = "KZpp bbbk kkkk kkkk kkkk",
                    CountryName = "Kazakhstan",
                }
            },

            {
                "QA", new IBANCountryInfo
                {
                    IBANReadableFormat = "QApp bbbb kkkk kkkk kkkk kkkk kkkk k",
                    CountryName = "Qatar",
                }
            },

            {
                "CG", new IBANCountryInfo
                {
                    IBANReadableFormat = "CGpp bbbb bsss sskk kkkk kkkk kKK",
                    CountryName = "Congo (Brazzaville)",
                }
            },

            {
                "XK", new IBANCountryInfo
                {
                    IBANReadableFormat = "XKpp bbbb kkkk kkkk kkkk",
                    CountryName = "Kosovo",
                }
            },

            {
                "HR", new IBANCountryInfo
                {
                    IBANReadableFormat = "HRpp bbbb bbbk kkkk kkkk k",
                    CountryName = "Croatia",
                }
            },

            
            {
                "KW", new IBANCountryInfo
                {
                    IBANReadableFormat = "KWpp bbbb kkkk kkkk kkkk kkkk kkkk kk",
                    CountryName = "Kuwait",
                }
            },

            {
                "LV", new IBANCountryInfo
                {
                    IBANReadableFormat = "LVpp bbbb kkkk kkkk kkkk k",
                    CountryName = "Latvia",
                }
            },

            {
                "LB", new IBANCountryInfo
                {
                    IBANReadableFormat = "LBpp bbbb kkkk kkkk kkkk kkkk kkkk",
                    CountryName = "Lebanon",
                }
            },

            {
                "LI", new IBANCountryInfo
                {
                    IBANReadableFormat = "LIpp bbbb bkkk kkkk kkkk k",
                    CountryName = "Liechtenstein",
                }
            },

            {
                "LT", new IBANCountryInfo
                {
                    IBANReadableFormat = "LTpp bbbb bkkk kkkk kkkk",
                    CountryName = "Lithuania",
                }
            },

            {
                "LU", new IBANCountryInfo
                {
                    IBANReadableFormat = "LUpp bbbk kkkk kkkk kkkk",
                    CountryName = "Luxembourg",
                }
            },

            {
                "MG", new IBANCountryInfo
                {
                    IBANReadableFormat = "MGpp bbbb bsss sskk kkkk kkkk kKK",
                    CountryName = "Madagaskar",
                }
            },

            {
                "ML", new IBANCountryInfo
                {
                    IBANReadableFormat = "MLpp bbbb bsss sskk kkkk kkkk kkKK",
                    CountryName = "Mali",
                }
            },

            {
                "MT", new IBANCountryInfo
                {
                    IBANReadableFormat = "MTpp bbbb ssss skkk kkkk kkkk kkkk kkk",
                    CountryName = "Malta",
                }
            },

            {
                "MR", new IBANCountryInfo
                {
                    IBANReadableFormat = "MRpp bbbb bsss sskk kkkk kkkk kKK",
                    CountryName = "Mauritania",
                }
            },

            {
                "MU", new IBANCountryInfo
                {
                    IBANReadableFormat = "MUpp bbbb bbss kkkk kkkk kkkk kkkK KK",
                    CountryName = "Mauritius",
                }
            },

            {
                "MK", new IBANCountryInfo
                {
                    IBANReadableFormat = "MKpp bbbk kkkk kkkk kKK",
                    CountryName = "Macedonia",
                }
            },

            {
                "MD", new IBANCountryInfo
                {
                    IBANReadableFormat = "MDpp bbkk kkkk kkkk kkkk kkkk",
                    CountryName = "Moldova",
                }
            },

            {
                "MC", new IBANCountryInfo
                {
                    IBANReadableFormat = "MCpp bbbb bsss sskk kkkk kkkk kKK",
                    CountryName = "Monaco",
                }
            },

            {
                "ME", new IBANCountryInfo
                {
                    IBANReadableFormat = "MEpp bbbk kkkk kkkk kkkk KK",
                    CountryName = "Montenegro",
                }
            },

            {
                "MZ", new IBANCountryInfo
                {
                    IBANReadableFormat = "MZpp bbbb ssss kkkk kkkk kkkK K",
                    CountryName = "Mozambique",
                }
            },

            {
                "NL", new IBANCountryInfo
                {
                    IBANReadableFormat = "NLpp bbbb kkkk kkkk kk",
                    CountryName = "Netherlands",
                }
            },

            {
                "NO", new IBANCountryInfo
                {
                    IBANReadableFormat = "NOpp bbbb kkkk kkK",
                    CountryName = "Norway",
                }
            },

            {
                "AT", new IBANCountryInfo
                {
                    IBANReadableFormat = "ATpp bbbb bkkk kkkk kkkk",
                    CountryName = "Austria",
                }
            },

            {
                "TL", new IBANCountryInfo
                {
                    IBANReadableFormat = "TLpp bbbk kkkk kkkk kkkk kKK",
                    CountryName = "East Timor",
                }
            },

            {
                "PK", new IBANCountryInfo
                {
                    IBANReadableFormat = "PKpp bbbb rrkk kkkk kkkk kkkk",
                    CountryName = "Pakistan",
                }
            },

            {
                "PS", new IBANCountryInfo
                {
                    IBANReadableFormat = "PSpp bbbb rrrr rrrr rkkk kkkk kkkk k",
                    CountryName = "Palestinian Territories",
                }
            },

            {
                "PL", new IBANCountryInfo
                {
                    IBANReadableFormat = "PLpp bbbs sssK kkkk kkkk kkkk kkkk",
                    CountryName = "Poland",
                }
            },

            {
                "PT", new IBANCountryInfo
                {
                    IBANReadableFormat = "PTpp bbbb ssss kkkk kkkk kkkK K",
                    CountryName = "Portugal",
                }
            },

            {
                "RO", new IBANCountryInfo
                {
                    IBANReadableFormat = "ROpp bbbb kkkk kkkk kkkk kkkk",
                    CountryName = "Romania",
                }
            },

            {
                "SM", new IBANCountryInfo
                {
                    IBANReadableFormat = "SMpp Kbbb bbss sssk kkkk kkkk kkk",
                    CountryName = "San Marino",
                }
            },

            {
                "ST", new IBANCountryInfo
                {
                    IBANReadableFormat = "STpp bbbb ssss kkkk kkkk kkkK K",
                    CountryName = "Sao Tome and Principe",
                }
            },

            {
                "SA", new IBANCountryInfo
                {
                    IBANReadableFormat = "SApp bbkk kkkk kkkk kkkk kkkk",
                    CountryName = "Saudi Arabia",
                }
            },

            {
                "SE", new IBANCountryInfo
                {
                    IBANReadableFormat = "SEpp bbbk kkkk kkkk kkkk kkkK",
                    CountryName = "Sweden",
                }
            },


            {
                "CH", new IBANCountryInfo
                {
                    IBANReadableFormat = "CHpp bbbb bkkk kkkk kkkk k",
                    CountryName = "Switzerland",
                }
            },

            {
                "SN", new IBANCountryInfo
                {
                    IBANReadableFormat = "SNpp bbbb bsss sskk kkkk kkkk kkKK",
                    CountryName = "Senegal",
                }
            },

            {
                "RS", new IBANCountryInfo
                {
                    IBANReadableFormat = "RSpp bbbk kkkk kkkk kkkk KK",
                    CountryName = "Serbia",
                }
            },

            {
                "SK", new IBANCountryInfo
                {
                    IBANReadableFormat = "SKpp bbbb ssss sskk kkkk kkkk",
                    CountryName = "Slovakia",
                }
            },

            {
                "SI", new IBANCountryInfo
                {
                    IBANReadableFormat = "SIpp bbss skkk kkkk kKK",
                    CountryName = "Slovenia",
                }
            },

            {
                "ES", new IBANCountryInfo
                {
                    IBANReadableFormat = "ESpp bbbb ssss KKkk kkkk kkkk",
                    CountryName = "Spain",
                }
            },

            {
                "CZ", new IBANCountryInfo
                {
                    IBANReadableFormat = "CZpp bbbb kkkk kkkk kkkk kkkk",
                    CountryName = "Czech Republic",
                }
            },

            {
                "TN", new IBANCountryInfo
                {
                    IBANReadableFormat = "TNpp bbss skkk kkkk kkkk kkKK",
                    CountryName = "Tunisia",
                }
            },

            {
                "TR", new IBANCountryInfo
                {
                    IBANReadableFormat = "TRpp bbbb brkk kkkk kkkk kkkk kk",
                    CountryName = "Turkey",
                }
            },

            {
                "HU", new IBANCountryInfo
                {
                    IBANReadableFormat = "HUpp bbbs sssK kkkk kkkk kkkk kkkK",
                    CountryName = "Hungary",
                }
            },

            {
                "AE", new IBANCountryInfo
                {
                    IBANReadableFormat = "AEpp bbbk kkkk kkkk kkkk kkk",
                    CountryName = "United Arab Emirates",
                }
            },

            {
                "GB", new IBANCountryInfo
                {
                    IBANReadableFormat = "GBpp bbbb ssss sskk kkkk kk",
                    CountryName = "United Kingdom (including Jersey, Guernsey, Isle of Man)",
                }
            },

            {
                "CY", new IBANCountryInfo
                {
                    IBANReadableFormat = "CYpp bbbs ssss kkkk kkkk kkkk kkkk",
                    CountryName = "Cyprus",
                }
            },

            {
                "CF", new IBANCountryInfo
                {
                    IBANReadableFormat = "CFpp bbbb bsss sskk kkkk kkkk kKK",
                    CountryName = "Central African Republic",
                }
            },
        };
    }
}