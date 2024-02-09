namespace ConsoleHttpClientLoripsum1feb2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CallWebAPIAsync()
        .Wait();
        }

        static async Task CallWebAPIAsync()
        {
            using (var httpClient = new HttpClient())
            {
                int? numberOfParagraphs = 3;

                string apiUrl = "https://loripsum.net/api";
                if (numberOfParagraphs.HasValue)
                {
                    apiUrl += "/" + numberOfParagraphs.ToString();
                }

                string length = "long"; // short // medium
                if (length != null)
                {
                    apiUrl += "/" + length.ToString();
                }

                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    // Process the response data here
                    await Console.Out.WriteLineAsync(responseContent);
                }

            }

            /*
<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quid ergo attinet gloriose loqui, nisi constanter loquare? Cum autem paulum firmitatis accessit, et animo utuntur et sensibus conitunturque, ut sese erigant, et manibus utuntur et eos agnoscunt, a quibus educantur. Istam voluptatem perpetuam quis potest praestare sapienti? Duo Reges: constructio interrete. Idem fecisset Epicurus, si sententiam hanc, quae nunc Hieronymi est, coniunxisset cum Aristippi vetere sententia. Non enim, si omnia non sequebatur, idcirco non erat ortus illinc. Sed emolumenta communia esse dicuntur, recte autem facta et peccata non habentur communia. Si enim ita est, vide ne facinus facias, cum mori suadeas. Morbo gravissimo affectus, exul, orbus, egens, torqueatur eculeo: quem hunc appellas, Zeno? Se omnia, quae secundum naturam sint, b o n a appellare, quae autem contra, m a l a. Sed haec ab Antiocho, familiari nostro, dicuntur multo melius et fortius, quam a Stasea dicebantur. Is hoc melior, quam Pyrrho, quod aliquod genus appetendi dedit, deterior quam ceteri, quod penitus a natura recessit. Ista ipsa, quae tu breviter: regem, dictatorem, divitem solum esse sapientem, a te quidem apte ac rotunde; Maximeque eos videre possumus res gestas audire et legere velle, qui a spe gerendi absunt confecti senectute. </p>

<p>Legimus tamen Diogenem, Antipatrum, Mnesarchum, Panaetium, multos alios in primisque familiarem nostrum Posidonium. An vero displicuit ea, quae tributa est animi virtutibus tanta praestantia? Aristoteles, Xenocrates, tota illa familia non dabit, quippe qui valitudinem, vires, divitias, gloriam, multa alia bona esse dicant, laudabilia non dicant. Atqui perspicuum est hominem e corpore animoque constare, cum primae sint animi partes, secundae corporis. Virtutis enim beataeque vitae, quae duo maxime expetenda sunt, serius lumen apparet, multo etiam serius, ut plane qualia sint intellegantur. Nihil enim hoc differt. Negat enim summo bono afferre incrementum diem. Vives, inquit Aristo, magnifice atque praeclare, quod erit cumque visum ages, numquam angere, numquam cupies, numquam timebis. Quid tibi, Torquate, quid huic Triario litterae, quid historiae cognitioque rerum, quid poetarum evolutio, quid tanta tot versuum memoria voluptatis affert? Et summatim quidem haec erant de corpore animoque dicenda, quibus quasi informatum est quid hominis natura postulet. Qui si omnes veri erunt, ut Epicuri ratio docet, tum denique poterit aliquid cognosci et percipi. Et saepe officium est sapientis desciscere a vita, cum sit beatissimus, si id oportune facere possit, quod est convenienter naturae. Itaque a sapientia praecipitur se ipsam, si usus sit, sapiens ut relinquat. Qui autem voluptate vitam effici beatam putabit, qui sibi is conveniet, si negabit voluptatem crescere longinquitate? </p>

<p>Ergo infelix una molestia, fellx rursus, cum is ipse anulus in praecordiis piscis inventus est? Nihil enim possumus iudicare, nisi quod est nostri iudicii-in quo frustra iudices solent, cum sententiam pronuntiant, addere: si quid mei iudicii est; Nec enim figura corporis nec ratio excellens ingenii humani significat ad unam hanc rem natum hominem, ut frueretur voluptatibus. Hoc igitur quaerentes omnes, et ii, qui quodcumque in mentem veniat aut quodcumque occurrat se sequi dicent, et vos ad naturam revertemini. Sed finge non solum callidum eum, qui aliquid improbe faciat, verum etiam praepotentem, ut M. Sed utrum hortandus es nobis, Luci, inquit, an etiam tua sponte propensus es? An hoc usque quaque, aliter in vita? </p>*/

            Console.Read();
        }
    }


}
