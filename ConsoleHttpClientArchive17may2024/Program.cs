using System.Text.Json;

namespace ConsoleHttpClientArchive17may2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // https://archive.org/help/aboutsearch.htm

            using var client = new HttpClient();

            string requestUri = "https://archive.org/advancedsearch.php?q=subject:palm+pilot+software&output=json&rows=100&page=5";

            HttpResponseMessage response = await client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            var resp = await response.Content.ReadAsStringAsync();

            //await Console.Out.WriteLineAsync(resp);

            // Create empty file in VS -> Paste Special -> Paste JSON as class

            Rootobject rootobject = JsonSerializer.Deserialize<Rootobject>(resp);

            Responseheader header = rootobject.responseHeader;
            Response object_response = rootobject.response;

            Doc[] docs = object_response.docs;

            int length = docs.Length;

            await Console.Out.WriteLineAsync("length=" + length);

            foreach (Doc doc in docs)
            {
                await Console.Out.WriteLineAsync("title = " + doc.title);
                await Console.Out.WriteLineAsync("description = " + doc.description + "\n");
            }

            /*length=100
title = PDilemma
description = This software is a simulation of the Iterated Prisoner's Dilemma. Its iterated match-up is not one-to-one, but rather a random selection based on geometric rules. That makes to be similar to a more realistic arms race.

title = Mobile High Speed
description = This software stores more than 200 network settings needed to go online with virtually any GPRS network, and it supports more than 50 phones. It can completely configure your Palm to use an EDGE/GPRS or HSCSD/GSM connection.

title = AstroWorld Suite
description = This astrology software will produce horoscopes and foretell astrological daily forecasts. It offers both text and graphical outputs for easy reading. This program is for Handheld PCs with a MIPS processor. Register this program if you want a daily forecast.

title = Netwalk
description = In this brain teaser, your task is to add order to a disconnected world and get every computer attached to a server. You can visually follow the signal spreading along the net when you adjust the connection cables by rotating them. Note: To buy this software:

title = E-Saver
description = E-Saver lets you save passwords and codes that might be important. You can save codes and passwords for thing like: Web sites Software Domains E-mail Hardware Note: This program requires Palm OS® 2.0 or later.

title = Plucker
description = This program allows you to read news or Web pages on your Palm, at any time, by downloading the news from the Internet or your local system, converting it with Plucker, and sending it to your Palm with our Plucker desktop software or your normal Palm tools.

title = EZBank (5mx/MC218/Series 7/netBook)
description = This banking and financial management software helps you manage your everyday accounts. Features are also available for foreign transactions, statement reminders and spending patterns. Note: This program is fully compatible with Series 5mx, Ericsson MC218, Series 7 and netBook.

title = Automotive OBD II DTC Search
description = Forget searching through thick automotive repair manuals trying to find an error code to explain why your vehicles "Check Engine" light is on. Auterras DTC search software interactively displays Diagnostic Trouble Code definitions. You can determine the exact meaning of the error.

title = OmniTimer
description = The software is designed for easy and fast managing of task events. It sets events in time to elapse rather than time of a day, much like cooking timer. OmniTimer has four independent time items, and each item is maintained independently.

title = BookBag Plus
description = This software keeps track of books and magazines that you own, have read, have checked out, and want to read or buy. You can use it to determine when books are due, and it includes multiple sort and filter options.

title = bShopper
description = With this shopping planner tool. With the software you can: Set costs for each item and plan what items you need Generate on-screen reports Print reports Export shopping reports to MemoPad Note: This application supports color.

title = PowerSet
description = The PowerSet is an innovative scientific Newton calculator that was designed to take advantage of the Newton platform. It offers abilities better than most desk calculators and features comparable to those you would expect in software for desktop computers.

title = Dots
description = Dots! -- A free game for your handheld and Palm-size PC! Even the hardest-working person needs a break once in a while. The next time you're stuck in a waiting room, or an airport, or in your boss's office waiting for him to get off the phone, try a quick game of Dots! Its fast, fun and free, from all of us at Ilium Software (who have spent way too much of our lives waiting). Dots is one of the games included in GameBag One, eight great games for Handheld and Palm-size PCs from Ilium Software.

title = TealScript
description = TealScript is an improved text-recognition system that is configurable, allowing for custom strokes and fine-tuning that is specially-tailored for each user. Visit the Software Page for free alternate stroke profiles. The program now has transparent color icons.

title = LCD Analyzer
description = LCD Analyzer allows you to check the quality of your Palm LCD. It can be used to find dead pixels or dust on your LCD. Color and high-resolution devices are supported. Note: This software only supports Palm and Sony devices with Palm OS® version 3.5 or above.

title = Shutdown
description = Shutdown is a simple program that allows you to turn off your Pocket PC via software. You can also select Lock or UnLock on the PocketPC, so that when you next turn on your Pocket PC, you will be prompted for a PIN.

title = enotate-fe
description = New! The shareware special edition of the award-winning enotate(tm) software! enotate-fe turns your Palm(tm) handheld into a direct, real-time extension of your PC. With enotate software, what you enter on your handheld shows up instantly on your PC. You can annotate digital photos, create diagrams and sketches and more, directly with your PC, using your handheld - just like you would with traditional pen and paper. You can use enotate software like a digital napkin to electronically capture and sketch your creative ideas directly into your PC, or mark up any graphic images in JPEG, BMP, TIFF and PCX formats. And, with the full version of enotate, you can directly mark up and annotate Microsoft Word and PowerPoint documents! The full version of enotate retails for $49.99. Visit the Web site at http://www.informal.com.

title = A Day In The Life
description = This game places you in the the life of a harried software engineer who must make choices, mostly between a rock and a hard place. At the end the game gives you a personality profile based on your choices.

title = Landing
description = The last thing a pilot needs to worry about on final approach is a dropped stylus in the cockpit! The Landing program can help. Landing is designed to provide airplane pilots with pattern headings for both left and right traffic at any airport. In addition, Landing displays current wind directions and velocities while calculating head/tailwind and crosswind components. Designed with the aviator in mind, Landing seeks to make the stylus a thing of the past.

title = SmallTalk
description = SmallTalk is the world's first two-way foreign language translation software. This 30 day demo version includes English and Pig Latin, allowing you to experience the full functionality of SmallTalk. The normal commercial version of SmallTalk includes English, Spanish, French, German and Italian.

title = Eric Snider's Solitaire
description = This is a collection of 10 solitaire games for your Palm device. It includes Carpet, Casino Klondike, Eight Off, Golf, Klondike, Montana, Poker Square, Pyramid, Spiderette and Towers. The software offers several backgrounds, win and loss statistics and sound effects.

title = I-Ching
description = This software generates a primary hexagram, which can be transformed into the relating, opposing and hidden hexagrams. Although a short description of each hexagram is provided, the user will probably want to refer to her favorite translation of the I-Ching.

title = PocketDBA
description = This software provides real-time management of your company's servers, databases and networks, from your BlackBerry handheld. It provides secure mobile control of multiple database platforms from the same console. Charts, graphs, trend analysis and text data rendering are supported.

title = RegTracker Corporate
description = This application keeps the names of all of your registered programs in one place. List expiration dates, codes and types of software. The information is beamable, so you can let friends know about programs that might be of interest to them. You can use this application on color and monochrome devices.

title = Juno Air
description = Access your Juno e-mail account using your Palm VII(tm) handheld. Read and write your Juno e-mail from anywhere at the touch of a button. Just download your Juno Air software today! Note: This PQA has only been tested on a Palm VII.

title = JDesignerPro
description = JDesignerPro is one of the first widely available RAD tools that allows anyone, even those without programming experience, to create wireless handheld enterprise applications. JDesignerPro is great for building mobile applications for industries including but not limited to: Sales Force Automation Warehousing Quoting Point of Sale software

title = Cybozu Sync 3
description = With Cybozu Sync 3, you can use one-touch synchronization capability for schedules, address records and to-do items on Palm handheld devices. All you need is a server running Cybozu Office 3 and a Windows-based client PC running Palm Desktop Software 3.X.

title = eyecontact
description = This application is a great address book. You can enter any data you need to keep, such as e-mail addresses, phone numbers, addresses and more. Using an eyemodule digital camera, you can capture images, as well as beam an ad address with an image to other units using the same software.

title = CasinoStud
description = CasinoStud is a collection of four popular casino-type Stud Poker games from Rose Software: CaribStud , DoubleDown , LetItRide and ThreeCard . CasinoStud features a shared bankroll, an option allowing easier switching between games, detailed hand statistics and on-screen help. Note: The program now supports color.

title = My Games
description = MyGame's holds information like price, date purchased, type and other information, it also shows at a glance what your collection is worth and how many game titles you have. MyGame's is simple to use, If you can switch your PDA on, then you can use this software.

title = HandTox
description = This is a calculator for the correct dose of N-Acetylcysteine. NAC is the main antidote for acetaminophen overdose. The software currently calculates for the 20 percent solution, which is most commonly used. Loading dose, maintanance dose and mode of administration are given.

title = Ur Price Checker
description = This program allows you to choose one of the ten available categories: music, classical, software, DVDs, videos, VHS, electronics, PC hardware, photos and books. You can then provide the UPC or ISBN number for the product, and press "Go!" The program displays a small picture of the product and all available pricing information.

title = PocketC
description = PocketC is a development tool for handheld devices. You can develop software on the device directly without using desktop. The source code is compiled into bytecode which is CPU independent, and can be run on all WinCE devices with our runtime module.

title = Handlix DRAW
description = This is a vector based drawing software which allows you to draw pictures, graphics, logotypes, charts, layouts or maps. It has a freehand function and allows you to export to other applications. It also includes a graphic symbol library and user defined fonts.

title = NoteTrain
description = NoteTrain is a program for PalmOS devices that is designed to teach you the names and pitches of the notes on a musical staff. NoteTrain has won an "Honorable Mention" in the Center for Innovative Learning Technologies' (CILT) Palm Educational Software contest.

title = GeoPDA
description = GeoPDA solutions enable you to transform your Palm device into a versatile field-mapping and utility kit with bi-directional support for ESRI ArcView shapefiles and connectivity to desktop, local area network (LAN), intranet or Internet databases. GeoPDA software includes: GeoGIS, GeoGPS, GeoPIX, GeoSync, Shp2PDB, and PDB2Shape for Windows.

title = N90 Buddy
description = Now you can attach your organizer to your Nikon N90/N90s camera! N90 Buddy connects to your Nikon N90/N90s using a Palm(tm) handheld serial cable and the Nikon MC-31 cable. N90 Buddy allows you to turn on the camera's memo holder, download frame exposure information, and set a number of custom options previously found in the Sharp Wizard/AC-nE card, OPTN90S shareware, Nikon Datalink software, and Nikon Photo Secretary software. Update: The N90 Buddy package now includes N90 Buddy Companion 1.0.3, which fixes a problem with converting databases with only one roll. Note: Additional equipment is needed please see the read me file after unzipping the program.

title = Pocket Casino
description = Pocket Casino is a package of three casino games that are graphically appealing and true to life. Choose between Black Jack, Video Poker or Slot Machine. Pocket casino has fun sounds, animation and a help file. Note: To buy this software which includes all three games :

title = Installigent
description = This keeps your Palm software up-to-date. Just install the application with PC conduit and sign in at the Web site. When you do HotSync, the Installigent Service will automatically deliver all the updates directly to your Palm, replacing out-of-date files with new ones.

title = LittleSense
description = LittleSense is a concise dictionary for your Palm handheld computer. Memory compression software makes this an efficient dictionary. There is a range of different dictionary databases to use with it, from 13,000 words right up to 122,000 words!

title = SlantAlpha v.3.5
description = This GPS software provides a continuous display of distances and bearings from the nearest airports, navaids and VFR waypoints. The visual layout is in exactly the form you need for communications with ATC, CTAF or FSS. Communicating your precise distance and compass direction from reporting points improves safety.

title = T-Force Platoon
description = Masterfully done, this classic Cannon-like game has been redesigned for the 21st century. Beautiful graphics and smooth precision play make for an intense bout of aim and fire. Choose and buy your weapon of destruction and buy your ammo too. Bravo! Note: To purchase this software:

title = Seahorse Challenge Pack
description = This all-in-one package is great for people who are new to Seahorse Software products. The package includes the latest versions of Crazy 8's, Euchre and Rummy. If you like packages, registering this program provides registered versions of all three games for a discounted price!

title = Visual Application Builder
description = PCForm (the Visual Application Builder) is a Visual front end for OrbWorks PocketC. The application is designed visually using the stylus to draw controls on the screen. The properties for the controls can be amended and code entered to handle control events. The software is written in PocketC and runs on the handheld device. The application calls the PocketC compiler to build the generated code. Applications can be opened and re-edited using PCForm, or they can be edited using PocketC. The software supports all the standard Windows controls, a subset of the Windows Common Controls (status bar, track bar, progress bar and up-down control), a number of drawing objects and the program menus. Note: Requires Pocket C.

title = Portfolio Manager
description = This portfolio management software (stocks) helps you to keep track of the transactions you have made, the profit and loss, and the overall status of your portfolio. In addition, the program will calculate the commission automatically and the profit and loss can be accurately reflected.

title = AudioList Plus
description = This software organizes audio, CD and music inventories. You can use it to keep track of collections items that you own, have listened to, have loaned or want to buy. You can download CD information from the Internet, and the program includes multiple sort and filter options.

title = IntelliGOLF Par
description = IntelliGolf. v4.1 adds more fun to golf by automating golf scoring, capturing round statistics and adding up your wagers (e.g. Skins) while youre on the course! Throw away pencil/paper golf scoring and let IntelliGolf and your Windows CE-based Palm-size PC do the work for you. The Par edition includes: * Automated golf scoring; * On-screen statistics and graphs, * Seventeen included sidegames, and * HoleView and 18 Hole ScorecardView displays; For even more functionality look to the IntelliGolf Birdie edition. It includes all of the Pars features plus powerful PC software for tracking historical round performance, approximating your handicap, and accessing +5,000 popular golf courses. Round set-up is a breeze. Try IntelliGolf today - Golfs #1 Scorecard Software!

title = Cardio Coach
description = The Cardio Coach is a heart rate measurement tool that features a maximum heart rate calculator. During your workout, you enter data and receive feedback to help you get the most out of your training. The software can also graph your session. Note: Color devices are supported.

title = PalmZip
description = Running out of memory? Even with newer Palm devices, there is always a thirst for more memory. This utility lets you pack and compress PRC files -- third party software programs -- so they consume less space on your device. PalmZip is compatible with both FlashPro and FlashBuilder by TRG/HandEra.

title = IntelliGolf for the Visor
description = Golfs No. 1 scorecard software is now available for the Handspring Visor on a Springboard-compatible module (or via an Internet download). Just plug the new IntelliGolf. module into your Visor to automate golf scoring, capture round statistics and add up wagers using any of its 17 included sidegames. Plus, you are also entitled to a free download of IntelliGolfs powerful Windows-based desktop software. It tracks on-going Round performance, approximates your handicap, provides 63 categories of game-improving statistics, and offers up more than 6,000 popular golf courses through its IntelliCourse database feature. IntelliGolf is also available for all Palm(tm) handhelds and Windows CE devices. Note: 3 Rounds of Golf Limited to 3 Rounds of Golf

title = Way of the Cross
description = A software Way of the Cross, with inspirational graphics, meditations, and prayers. Click the arrow buttons or use the scroll buttons to move through the stations. Click the document icon to view meditations and prayers. Note: File when installed on handheld is called Stations, for 'Stations of the Cross'.

title = Palm OS Gnu Dev Tools Prebuilt VM
description = https://github.com/meepingsnesroms/prc-tools-remix The VM was too big for github. To any staff considering deleting this, there is almost nothing remaining of the old Palm OS tools, this is likely the only working build you dont have to compile yourself. Username: PalmOSDev Password:  palmosdev

title = Hydraulic Engineering Tools
description = This application remembers complicated hydraulics formulas. Enter values and see the solutions within seconds. This application was formed by Hydraulic Engineers who collected 32 of the most used calculators for software like this. This application supports the English and Metric Systems.

title = Pocket Register
description = Pocket Register is a portable invoicing software package for the Palm III(tm) handheld. It uses the barcode scanner on the Symbol SPT- 1500 Palm unit and writes up receipts. It will print receipts to an infrared port on a printer using optional Stevens Creek PalmPrint printer driver.

title = PocketC
description = PocketC is a development tool for handheld devices. The program includes a compiler, which can generate CPU independent software with an extremely small file size. PocketC also provides a robust function library, including graphics, sound, database I/O, and serial I/O, as well as Dynamic Memory Allocation functionality.

title = Back It Up
description = Back It Up will help you on the road to give you quick sizing for the number of tape drives required for your backup. It will tell you how much throughput is required in meg per second and GB per hour. The software can also take account of multiple drives.

title = Xircuit Contact for PPC - New and Improved Contacts Manager
description = This is a replacement application for the built-in Contacts function. It is fully compatible with Pocket Outlook and the synchronization mechanism. It features a unique collection of more than 300 pre-defined text blocks for fast input. The software features a wide range of other features too.

title = pTextweb
description = This is a hypertext notepad with a reporting and cross-referencing feature. It is useful for things like designing or reverse-engineering software; planning or analyzing a book, play, movie, or Web site; creating a branching text for use as a teleprompter in a speech; documenting procedures; or planning for contingencies.

title = Snappixx
description = This is remote snapshot software for Nikon Coolpix digital cameras. It provides remote snapshots, manual bulb shots, automatic bulb shots (from 1 to 60 seconds), retrieval of camera information, intervalled snapshot functionality, sleep mode during interval to save the camera's battery, zoom in and out, navigation in review mode, night vision mode.

title = TimeReporter 2000
description = TimeReporter 2000, a time-and-expense-tracking software package, is designed for consultants, lawyers, foremen and other business professionals who need to capture time and expense data on their handheld and PC. TimeReporter integrates with Quicken, Quickbooks Pro, Microsoft Excel and Access, as well as other applications. Note: Requires a demo trial license .

title = PalmDoc
description = PalmDoc is a simple software program that's designed to shorten the time it takes to convert a text document to a format that is readable by a handheld. PalmDoc converts files to .pdb files, making them readable via doc reader programs. (This latest version contains minor bug fixes.)

title = SWMate
description = SWMate allows you track the list of software you have purchased. Features: Editable category, month and payment type Different views and filters Auto total of the amount Auto calculate the amount based on price and quantity Toolbox for sorting, exporting to memo and beaming Note: This program requires Palm OS® 3.1 or above.

title = P.T. Golf
description = Install P.T. Golf's easy to use score card on your palmtop, laptop, or desktop PC and you're ready to play. The easy-to-use palmtop software will keep your scores while you play and it won't slow down your game either. When you're finished playing, simply upload the scores to your desktop computer and your ready to analyze your game.

title = MyDoktor
description = This medical and drug EPOC database has more than 1500 monographs. There is a comprehensive drugs list which is updated monthly. The medical notes have ECGs, x-rays and graphs. Note: The file downloaded here is only for palmtop computers with EPOC operating systems. The zipped file cannot be extracted with WinZip or other software not designed for EPOC operating system.

title = Sentry 2020 - Essential security upgrade
description = Sentry 2020 uses strong encryption algorithms to seamlessly encrypt files as they are being accessed by applications such as Word and Excel. The software is compatible with Sentry 2020 for Windows NT/2000, which allows the use of the same encrypted volumes on your desktop computer as on your mobile CE/PPC device.

title = Video Librarian
description = This program lets you index, catalogue and manage information about old and new video taped events. At playback time, with a single tap of the PDA screen, the program will instruct the video camera to wind the video tape to the exact location of choice. Note: Video Librarian software is U.S. patent pending.

title = IntelliGolf Par Edition
description = The IntelliGolf Par edition scorecard software automates golf scoring and wagering while you're out on the green. This program has automated golf scoring, statistics and graphs that are sure to improve your game. It includes course, hole and round notes, along with 24 popular wagering games. New to the updated version is wireless access to digital scorecards, beaming between handhelds and color displays.

title = HandWallet
description = This is a tool for tracking expense and income, and for planning your budget. It allows you to track your credit cards, bank accounts, saving accounts, loan coupons, mileage and cash transactions. Using this software you will be able to enter exchange rates and make currency conversion and get tax reports and financial graphs.

title = Daga
description = This game from StMick Software will test your ability to think logically under pressure! Your goal is to help Daga the mouse get all the cheese in the maze by rearranging the horizontal and vertical bars using the stylus pen. Beware of the many cheeses laced with poison that litter the maze. One bite is enough to kill Daga!

title = Clie Sounds FX
description = This is a set of alarms and sound effects for Sony Clie devices. It includes barking dogs, voice clips, Homer Simpson and Morpheus from the Matrix. You can use these alarms for DateBook, MegaClock, BugMe and other Palm software that supports Clie's System Sounds alarms.

title = BinCalc
description = BinCalc is a calculator designed for computer software and hardware engineers. BinCalc works with binary numbers eight, 16, 32 and 64 bits in length, and uses Reverse Polish Notation (RPN). BinCalc contains a five-element stack (including the display), and 10 elements of memory.

title = Flash!
description = Flash! is a very powerful learning tool. It's fully featured and based on the concept of flash cards. The Flash! "learning mode" conforms to the latest learning research by quizzing you on a personal schedule that adapts to specific learning difficulties. In "quiz mode," you can evaluate your learning progress. Flash! supports full on-pilot editing capabilities and the importing of JTutor databases. Quiz yourself using the honor system, or select a multiple-choice format. (Fifty percent of the income generated by sales of Flash! goes to charity!)

title = EnteralCalc
description = Health care professionals use this PDA based enteral feeding software to assign and keep track of patient histories, formula data, and patient caloric and protein requirements. It provides feeding volumes and rates, fluid needs, BMI, BSA and other important calculations. You can beam and print patient histories from the PDA.

title = Pocket TurboDoc Electronic Medical Record
description = This is electronic medical record software for the Pocket PC. Create consultations, H and P, and SOAP notes and customize word lists. The program includes a quick drug database with over 6,500 entries. It also includes a medical phone list (referring doctors and pharmacies). The full version supports printing capabilities and can track up to 2,000 patients' data.

title = Clever Bet, Dutch Betting System
description = This software is designed to assist those who place bets on horse races. If you want to make a bet on more than one horse, you to predict the return and minimize your risk. This lets you calculate your bets when you are at the race course, so you can make you decisions at the last minute.

title = Siberian Strike
description = Take off as a pilot during WW II and infiltrate a secret base in the heart of Siberia. This multi-featured game includes: There are seven original levels and two high-speed slaloms. You have your choice of three classic WWII fighters. There are colorful and detailed graphics with full Sound effects and arcade-style game play, complete with loads of bonuses and special effects. Five huge end-level enemy bosses await you as you progress through the game using hard-key, stylus on-screen or Graffiti area controls.

title = TinySheet
description = This Excel-like spreadsheet for Palm(tm) devices enables you to carry your PC spreadsheets with you. Capabilities include: Import and export spreadsheets (including tab and comma-delimited files with values or formulas) on PC and Macintosh 80 separate functions A pop-up, calculator-style keyboard Sort up to three columns with an option for row header Advanced formatting options Recognition of international settings Fast cell navigation Synchronization between your device and your PC Workbooks with unlimited sheets Note: There is a 3 day evaluation period before you accuire a license key for a 30 day demo. Get the license key at: http://www.iambic.com/pilot/tinysheet/download.htm .

title = PocketBilling
description = This software is an easy and fast way for physicians to keep track of patient billing and tracking. What's even better is that this application was designed and tested by people in the medical field. This program lets you change charges and keep track of patients. You can also customize this program to your specific needs.

title = MySports Soccer
description = This is a soccer team management suite for PocketPCs and desktops running .NET Framework. It provides complete game scheduling and history, including time, location, and rival team. It also offers player roster management, including player stats, player photo, and event history. Export complete team information to HTML and text files right on the PocketPC. The software also offers a report printing module integrated into the desktop suite.

title = Seahorse Game Pack 2
description = This all-in-one package is great for people who are new to Seahorse Software products. If you enjoy sampling multiple products, pay attention: Registering for Seahorse Game Pack 2 gets you registered versions of all the latest programs, all for a discounted price! Seahorse Game Pack 2 includes: Rummy, BlackJack Solitaire and Montana Solitaire. Note: This update: Rummy 2.0, BlackJack Solitaire 2.0 and Montana Solitaire 2.0.

title = QuikInstall
description = QuikInstall lets you install software on your Palm OS handheld more quickly. Features include: Uses standard HotSync so no setup is required Temporarily disables all conduits so that the install will be very quick Requires only a single Return keystroke to complete the install Features a full uninstaller so it is easy to revert back to the Palm installer

title = Mobile Business 2003
description = This software bundle contains both Mobile Money 2002 and Mobile Access 2003. Mobile Money is a Palm OS personal finance assistant designed to help you manage and maintain your daily money flow. Mobile Access 2003 allows you to manage and create your own databases. The Microsoft Excel integrated desktop module allows you to export existing MS Excel worksheets.

title = Live: Lines - live sports odds
description = Get the latest real-time sports odds sent directly to your Palm VII(tm) handheld! Utilizes cyberoad.com's Version 2 Netbook software: the fastest, most flexible sports betting system ever developed. View lines for NBA Basketball, ML Baseball, NHL Hockey and NFL Football. Other sports and features coming soon. Note: This PQA has only been tested on a Palm VII.

title = D'Accord Mobile Chords
description = This program enables you to get guitar chords on your Palm OS handheld or smartphone. Just type the chord name, and the software will show the fingering and several positions. You can also listen to the chord notes. The built-in database has more than 1,000 chords .

title = UltraMoteB Universal Remote
description = This is universal remote control software for Pocket PCs. It lets users define the look and feel of their remotes, and create up to 25 device and activity screens for up to 10 different users. It includes support for the UltraMote Extender and the infrared add-in card for a range of up to 50 feet.

title = Intercue Professional
description = This program provides three software components that work together to assist in remote data collection. Intercue Designer enables you to build electronic forms and applications for mobile devices. Intercue Desktop provides desktop management and publishing for electronic forms, applications and data for the Intercue platform. Intercue Presenter administrates data collection for your electronic forms and applications on mobile devices.

title = DueDateCalc
description = DueDateCalc calculates due dates (EDC) and estimated gestational ages (EGA) based on last menstrual period (LMP), conception date, and ultrasound dates. It also features estimated fetal weight (EFW) percentiles between 22 and 44 weeks. Those in the Ob/Gyn practice may find this to be a very useful tool. Note: To purchase this software:

title = ExpenseDirector
description = ExpenseDirector is a powerful application that contains both a handheld component and a Windows desktop component (Windows component is 6 MB in size). This is the newest and most advanced expense-tracking iambic Software application. If you must keep up-to-date and accurate records of your business or personal expenses, ExpenseDirector is a must-have application. NOTE: Iambic Software offers a special bundled price of only $59.95 for both the handheld and PC applications. However, ExpenseDirector for the Palm IS a stand-alone application for the handheld device that can be purchased for $29.95. Note: To use your ExpenseDirector demo you need to apply for a 30 day demo license key .

title = Memo Safe
description = This is a 100-percent compatible replacement for the built-in Memo Pad application. With easy-to-use memo encryption, security and tons of advanced features, this is the perfect add-on to your Palm Pilot. Now you can read and write the same memo records as the built-in Memo application. It has every feature of the original Memo Pad along with the bonus of additional security. There is beaming support on Palm III or higher, find support, user configuration and a low price. This also includes a viewer application for Windows that enables private viewing of encrypted notes.

title = ALP
description = This program contains software designed to keep track of all your passwords. It's quick and easy-to-use! ALP features a graphical padlock, database security and password expiration tracking. Updates include: Color A new "Delete All" button for Conduit users An updated manual A new Graffiti shift icon Note: This program now supports color for the Palm IIIC handheld.

title = SharkPoint
description = If you have a handheld GPS unit with compatible data output, you can record the coordinates of dive sites to within feet. At home, SharkPoint for Palm synchronizes to your PC for data backup. If you also have the PC software, you can load and edit the data in detail. Statistics can be compiled and printed.

title = Pad
description = This application is still in Beta development -- as such, Tucows has not rated or reviewed the software. Pad lets you create sophiscated drawings for fun or professional use. Features developed to date include: Black and gray panes Pen support Shape Insertion Note: Fully compatible with ALL EPOC devices except Oregon Osaris. No rating given whilst in Beta development.

title = WinMobileZip
description = This compression software enables you to uncompress or create ZIP archives while you are on the road. Zip and send ZIP archives by e-mail in just a few seconds directly from the file explorer. You can use WinMobileZip to reclaim extra spaces from your mobile device by compressing rarely used files or folders.

title = Pocket Trip Tracker
description = This tool keeps track of automobile mileage for multiple vehicles. You can also categorize the logs as either personal or your business miles. Just make a single entry for each trip. It's that easy. You can also track your fuel consumption to determine the actual number of miles per gallon that your cars achieve. Note: To buy this software:

title = PocketGIS
description = PocketGIS(tm) software is an innovative, affordable and user-friendly GIS product that offers an excellent means of seamless field data capture. PocketGIS is available in a variety of languages, including French and German. Written on Windows CE, users can choose from a variety of handhelds. The rugged Husky FEX21 and Fujitsu Pencentra 130 models, which are suitable for field use, are highly recommended. Incorporating a powerful set of GIS functions, PocketGIS interfaces to all main corporate GIS systems for accurate data capture, as well as location tracking. PocketGIS has been designed to support the development of customised applications. The software can be tailored to your needs with written forms that are specific for collection requirements. GIS background mapping and attribute information can be accessed. Points, lines and polygons can be created. Existing features can be "red-lined."

title = SimpleTerm
description = SimpleTerm doesn't perform any terminal emulation, but it does allow you to use AT commands to connect to remote computer systems using a modem or serial cable. The Pilot can be used a with NullModem adapter, available from USR. A clipboard feature is included, but the clipboard has a limit of 1,000 characters. There is also a paste function, which copies the clipboard to the send buffer, but this function is limited to 40 characters. Connection speeds of 1200, 2400, 4800, 9600 or 19200 are available.

title = RMRExpense
description = RMRExpense is the extraction of the expense accounts manager system presently found in RMRBank. Quickly gain complete control of your expenses, work with multiple currencies and export as needed to various formats for reporting purposes. This continues the long tradition of excellent software from RMR. The program is available in English, French, Danish and Italian. Note: Fully compatible with ALL EPOC devices.

title = Sinclair Spectrum Emulator
description = A Sinclair Spectrum Emulator that works on any Palm-size PC, Handheld PC or H/PC pro, and is capable of running software (read "a mind-boggling number of games") developed for the Sinclair Spectrum - programs like Tetris, Pac Man, Lemmings, Elite, Star Glider, Donkey Kong, Chess, Lemans Racing, Jet Set Willy, Manic Miner, Sabre Wulf, Boulderdash, Jet Pac etc.

title = Scribble2000
description = Scribble uses a simplified alphabet, similar to Palm Graffiti, which is quite easy to learn. It works with most other software on all EPOC devices. Scribble2000 now includes: Improved character recoqnition Some punctuation Ability to rotate input space Resizable window, with three user-definable preset window sizes (and preferences). Note: No rating is given while the program is in beta testing.

title = ListMan
description = Need a task manager with more functionality than the one that came with your Pocket PC? ListMan is an option. With ListMan you can create hierarchical lists that support categories and subcategories. This intuitive software also offers you the ability to hide, purge and uncheck all checked items in a list.
*/


            Console.Read();
        }
    }
}
