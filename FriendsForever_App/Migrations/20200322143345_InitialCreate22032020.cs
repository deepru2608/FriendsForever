using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FriendsForever_App.Migrations
{
    public partial class InitialCreate22032020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true, computedColumnSql: "[LastName] + ', ' + [FirstName]"),
                    Doe = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    ProfilePhotoPath = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryMaster",
                columns: table => new
                {
                    CountryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(nullable: true),
                    CountryCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryMaster", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CountryMaster",
                columns: new[] { "CountryId", "CountryCode", "CountryName" },
                values: new object[,]
                {
                    { 1, "AF", "Afghanistan" },
                    { 158, "NC", "New Caledonia" },
                    { 159, "NZ", "New Zealand" },
                    { 160, "NI", "Nicaragua" },
                    { 161, "NE", "Niger" },
                    { 162, "NG", "Nigeria" },
                    { 163, "NU", "Niue" },
                    { 164, "NF", "Norfolk Island" },
                    { 165, "MP", "Northern Mariana Islands" },
                    { 166, "NO", "Norway" },
                    { 167, "OM", "Oman" },
                    { 168, "PK", "Pakistan" },
                    { 169, "PW", "Palau" },
                    { 170, "PS", "Palestine" },
                    { 171, "PA", "Panama" },
                    { 172, "PG", "Papua New Guinea" },
                    { 173, "PY", "Paraguay" },
                    { 174, "PE", "Peru" },
                    { 175, "PH", "Philippines" },
                    { 176, "PN", "Pitcairn" },
                    { 177, "PL", "Poland" },
                    { 178, "PT", "Portugal" },
                    { 179, "PR", "Puerto Rico" },
                    { 180, "QA", "Qatar" },
                    { 181, "RE", "RÃ©union" },
                    { 182, "RO", "Romania" },
                    { 183, "RU", "Russian Federation" },
                    { 184, "RW", "Rwanda" },
                    { 157, "NL", "Netherlands" },
                    { 185, "BL", "Saint Barthaemy" },
                    { 156, "NP", "Nepal" },
                    { 154, "NA", "Namibia" },
                    { 127, "LY", "Libya" },
                    { 128, "LI", "Liechtenstein" },
                    { 129, "LT", "Lithuania" },
                    { 130, "LU", "Luxembourg" },
                    { 131, "MO", "Macao" },
                    { 132, "MK", "Macedonia" },
                    { 133, "MG", "Madagascar" },
                    { 134, "MW", "Malawi" },
                    { 135, "MY", "Malaysia" },
                    { 136, "MV", "Maldives" },
                    { 137, "ML", "Mali" },
                    { 138, "MT", "Malta" },
                    { 139, "MH", "Marshall Islands" },
                    { 140, "MQ", "Martinique" },
                    { 141, "MR", "Mauritania" },
                    { 142, "MU", "Mauritius" },
                    { 143, "YT", "Mayotte" },
                    { 144, "MX", "Mexico" },
                    { 145, "FM", "Micronesia" },
                    { 146, "MD", "Moldova" },
                    { 147, "MC", "Monaco" },
                    { 148, "MN", "Mongolia" },
                    { 149, "ME", "Montenegro" },
                    { 150, "MS", "Montserrat" },
                    { 151, "MA", "Morocco" },
                    { 152, "MZ", "Mozambique" },
                    { 153, "MM", "Myanmar" },
                    { 155, "NR", "Nauru" },
                    { 126, "LR", "Liberia" },
                    { 186, "SH", "Saint Helena" },
                    { 188, "LC", "Saint Lucia" },
                    { 220, "TZ", "Tanzania" },
                    { 221, "TH", "Thailand" },
                    { 222, "TL", "Timor-Leste" },
                    { 223, "TG", "Togo" },
                    { 224, "TK", "Tokelau" },
                    { 225, "TO", "Tonga" },
                    { 226, "TT", "Trinidad and Tobago" },
                    { 227, "TN", "Tunisia" },
                    { 228, "TR", "Turkey" },
                    { 229, "TM", "Turkmenistan" },
                    { 230, "TC", "Turks and Caicos Islands" },
                    { 231, "TV", "Tuvalu" },
                    { 232, "UG", "Uganda" },
                    { 233, "UA", "Ukraine" },
                    { 234, "AE", "United Arab Emirates" },
                    { 235, "GB", "United Kingdom" },
                    { 236, "US", "United States" },
                    { 237, "UM", "Minor Outlying Islands" },
                    { 238, "UY", "Uruguay" },
                    { 239, "UZ", "Uzbekistan" },
                    { 240, "VU", "Vanuatu" },
                    { 241, "VE", "Venezuela" },
                    { 242, "VN", "Viet Nam" },
                    { 243, "VG", "Virgin Islands" },
                    { 244, "WF", "Wallis and Futuna" },
                    { 245, "EH", "Western Sahara" },
                    { 246, "YE", "Yemen" },
                    { 219, "TJ", "Tajikistan" },
                    { 187, "KN", "Saint Kitts and Nevis" },
                    { 218, "TW", "Taiwan" },
                    { 216, "CH", "Switzerland" },
                    { 189, "MF", "Saint Martin" },
                    { 190, "PM", "Saint Pierre and Miquelon" },
                    { 191, "VC", "Saint Vincent and the Grenadines" },
                    { 192, "WS", "Samoa" },
                    { 193, "SM", "San Marino" },
                    { 194, "ST", "Sao Tome and Principe" },
                    { 195, "SA", "Saudi Arabia" },
                    { 196, "SN", "Senegal" },
                    { 197, "RS", "Serbia" },
                    { 198, "SC", "Seychelles" },
                    { 199, "SL", "Sierra Leone" },
                    { 200, "SG", "Singapore" },
                    { 201, "SX", "Sint Maarten" },
                    { 202, "SK", "Slovakia" },
                    { 203, "SI", "Slovenia" },
                    { 204, "SB", "Solomon Islands" },
                    { 205, "SO", "Somalia" },
                    { 206, "ZA", "South Africa" },
                    { 207, "GS", "South Georgia" },
                    { 208, "SS", "South Sudan" },
                    { 209, "ES", "Spain" },
                    { 210, "LK", "Sri Lanka" },
                    { 211, "SD", "Sudan" },
                    { 212, "SR", "Suriname" },
                    { 213, "SJ", "Svalbard and Jan Mayen" },
                    { 214, "SZ", "Swaziland" },
                    { 215, "SE", "Sweden" },
                    { 217, "SY", "Syrian Arab Republic" },
                    { 125, "LS", "Lesotho" },
                    { 124, "LB", "Lebanon" },
                    { 123, "LV", "Latvia" },
                    { 33, "IO", "British Indian Ocean Territory" },
                    { 34, "BN", "Brunei Darussalam" },
                    { 35, "BG", "Bulgaria" },
                    { 36, "BF", "Burkina Faso" },
                    { 37, "BI", "Burundi" },
                    { 38, "KH", "Cambodia" },
                    { 39, "CM", "Cameroon" },
                    { 40, "CA", "Canada" },
                    { 41, "CV", "Cape Verde" },
                    { 42, "KY", "Cayman Islands" },
                    { 43, "CF", "Central African Republic" },
                    { 44, "TD", "Chad" },
                    { 45, "CL", "Chile" },
                    { 46, "CN", "China" },
                    { 47, "CX", "Christmas Island" },
                    { 48, "CC", "Cocos (Keeling) Islands" },
                    { 49, "CO", "Colombia" },
                    { 50, "KM", "Comoros" },
                    { 51, "CG", "Congo" },
                    { 52, "CD", "Congo" },
                    { 53, "CK", "Cook Islands" },
                    { 54, "CR", "Costa Rica" },
                    { 55, "CI", "CÃ´te d'Ivoire" },
                    { 56, "HR", "Croatia" },
                    { 57, "CU", "Cuba" },
                    { 58, "CW", "CuraÃ§ao" },
                    { 59, "CY", "Cyprus" },
                    { 32, "BR", "Brazil" },
                    { 60, "CZ", "Czech Republic" },
                    { 31, "BV", "Bouvet Island" },
                    { 29, "BA", "Bosnia and Herzegovina" },
                    { 2, "AX", "Aland Islands" },
                    { 3, "AL", "Albania" },
                    { 4, "DZ", "Algeria" },
                    { 5, "AS", "American Samoa" },
                    { 6, "AD", "Andorra" },
                    { 7, "AO", "Angola" },
                    { 8, "AI", "Anguilla" },
                    { 9, "AQ", "Antarctica" },
                    { 10, "AG", "Antigua and Barbuda" },
                    { 11, "AR", "Argentina" },
                    { 12, "AM", "Armenia" },
                    { 13, "AW", "Aruba" },
                    { 14, "AU", "Australia" },
                    { 15, "AT", "Austria" },
                    { 16, "AZ", "Azerbaijan" },
                    { 17, "BS", "Bahamas" },
                    { 18, "BH", "Bahrain" },
                    { 19, "BD", "Bangladesh" },
                    { 20, "BB", "Barbados" },
                    { 21, "BY", "Belarus" },
                    { 22, "BE", "Belgium" },
                    { 23, "BZ", "Belize" },
                    { 24, "BJ", "Benin" },
                    { 25, "BM", "Bermuda" },
                    { 26, "BT", "Bhutan" },
                    { 27, "BO", "Bolivia" },
                    { 28, "BQ", "Bonaire" },
                    { 30, "BW", "Botswana" },
                    { 61, "DK", "Denmark" },
                    { 62, "DJ", "Djibouti" },
                    { 63, "DM", "Dominica" },
                    { 96, "HT", "Haiti" },
                    { 97, "HM", "Heard and McDonald Islands" },
                    { 98, "VA", "Holy See" },
                    { 99, "HN", "Honduras" },
                    { 100, "HK", "Hong Kong" },
                    { 101, "HU", "Hungary" },
                    { 102, "IS", "Iceland" },
                    { 103, "IN", "India" },
                    { 104, "ID", "Indonesia" },
                    { 105, "IR", "Iran" },
                    { 106, "IQ", "Iraq" },
                    { 107, "IE", "Ireland" },
                    { 108, "IM", "Isle of Man" },
                    { 109, "IL", "Israel" },
                    { 110, "IT", "Italy" },
                    { 111, "JM", "Jamaica" },
                    { 112, "JP", "Japan" },
                    { 113, "JE", "Jersey" },
                    { 114, "JO", "Jordan" },
                    { 115, "KZ", "Kazakhstan" },
                    { 116, "KE", "Kenya" },
                    { 117, "KI", "Kiribati" },
                    { 118, "KP", "Korea" },
                    { 119, "KR", "Korea, Republic of" },
                    { 120, "KW", "Kuwait" },
                    { 121, "KG", "Kyrgyzstan" },
                    { 122, "LA", "Lao People's Democratic Republic" },
                    { 95, "GY", "Guyana" },
                    { 94, "GW", "Guinea-Bissau" },
                    { 93, "GN", "Guinea" },
                    { 92, "GG", "Guernsey" },
                    { 64, "DO", "Dominican Republic" },
                    { 65, "EC", "Ecuador" },
                    { 66, "EG", "Egypt" },
                    { 67, "SV", "El Salvador" },
                    { 68, "GQ", "Equatorial Guinea" },
                    { 69, "ER", "Eritrea" },
                    { 70, "EE", "Estonia" },
                    { 71, "ET", "Ethiopia" },
                    { 72, "FK", "Falkland Islands" },
                    { 73, "FO", "Faroe Islands" },
                    { 74, "FJ", "Fiji" },
                    { 75, "FI", "Finland" },
                    { 76, "FR", "France" },
                    { 247, "ZM", "Zambia" },
                    { 77, "GF", "French Guiana" },
                    { 79, "TF", "French Southern Territories" },
                    { 80, "GA", "Gabon" },
                    { 81, "GM", "Gambia" },
                    { 82, "GE", "Georgia" },
                    { 83, "DE", "Germany" },
                    { 84, "GH", "Ghana" },
                    { 85, "GI", "Gibraltar" },
                    { 86, "GR", "Greece" },
                    { 87, "GL", "Greenland" },
                    { 88, "GD", "Grenada" },
                    { 89, "GP", "Guadeloupe" },
                    { 90, "GU", "Guam" },
                    { 91, "GT", "Guatemala" },
                    { 78, "PF", "French Polynesia" },
                    { 248, "ZW", "Zimbabwe" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CountryMaster");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
