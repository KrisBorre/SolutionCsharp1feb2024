using System.Xml.Serialization;

namespace ConsoleHttpClientArxiv22may2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // arXiv.org hosts more than two million scholarly articles
            //https://info.arxiv.org/help/api/user-manual.html

            using var client = new HttpClient();

            string word = "p53";

            string requestUri = "http://export.arxiv.org/api/query?search_query=all:" + word;

            HttpResponseMessage response = await client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            string xmlString = await response.Content.ReadAsStringAsync();

            //await Console.Out.WriteLineAsync(xmlString);

            using (StringReader reader = new StringReader(xmlString))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(feed));
                feed feed = (feed)serializer.Deserialize(reader);

                feedTitle feed_title = feed.title;

                feedEntry[] feed_entry = feed.entry;

                string value = feed_title.Value;
                await Console.Out.WriteLineAsync("value = " + value);

                int length = feed_entry.Length;
                await Console.Out.WriteLineAsync("length = " + length);

                foreach (var entry in feed_entry)
                {
                    object[] items = entry.Items;

                    await Console.Out.WriteLineAsync("length = " + items.Length);

                    foreach (object item in items)
                    {
                        if (item is feedEntryAuthor)
                        {
                            string name = ((feedEntryAuthor)item).name;
                        }
                        else if (item is feedEntryLink)
                        {
                            string href = ((feedEntryLink)item).href;
                        }
                        else if (item is feedEntryCategory)
                        {
                            string term = ((feedEntryCategory)item).term;
                        }
                        else if (item is primary_category)
                        {
                            string term = ((primary_category)item).term;
                        }
                        else
                        {
                            await Console.Out.WriteLineAsync(item.ToString());
                        }
                    }
                }
            }

            /*
            <?xml version="1.0" encoding="UTF-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
              <link href="http://arxiv.org/api/query?search_query%3Dall%3Ap53%26id_list%3D%26start%3D0%26max_results%3D10" rel="self" type="application/atom+xml"/>
              <title type="html">ArXiv Query: search_query=all:p53&amp;id_list=&amp;start=0&amp;max_results=10</title>
              <id>http://arxiv.org/api/J1dWxABvrRCvs94a7UXj5RwHJ1s</id>
              <updated>2024-05-22T00:00:00-04:00</updated>
              <opensearch:totalResults xmlns:opensearch="http://a9.com/-/spec/opensearch/1.1/">79</opensearch:totalResults>
              <opensearch:startIndex xmlns:opensearch="http://a9.com/-/spec/opensearch/1.1/">0</opensearch:startIndex>
              <opensearch:itemsPerPage xmlns:opensearch="http://a9.com/-/spec/opensearch/1.1/">10</opensearch:itemsPerPage>
              <entry>
                <id>http://arxiv.org/abs/1503.08274v1</id>
                <updated>2015-03-28T07:05:10Z</updated>
                <published>2015-03-28T07:05:10Z</published>
                <title>PDCD5 interacts with p53 and functions as a regulator of p53 dynamics in
              the DNA damage response</title>
                <summary>  The tumor suppressor p53 plays a central role in cell fate decisions after
            DNA damage. Programmed Cell Death 5 (PDCD5) is known to interact with the p53
            pathway to promote cell apoptosis. Recombinant human PDCD5 can significantly
            sensitize different cancers to chemotherapies. In the present paper, we
            construct a computational model that includes PDCD5 interactions in the p53
            signaling network and study the effects of PDCD5 on p53-mediated cell fate
            decisions during the DNA damage response. Our results revealed that PDCD5
            functions as a co-activator of p53 that regulates p53-dependent cell fate
            decisions via the mediation of p53 dynamics. The effects of PDCD5 are
            dose-dependent such that p53 can display either sustained or pulsed dynamics at
            different PDCD5 levels. Moreover, PDCD5 regulates caspase-3 activation via two
            mechanisms during the two phases of sustained and pulsed p53 dynamics. This
            study provides insights regarding how PDCD5 functions as a regulator of the p53
            pathway and might be helpful for increasing our understanding of the molecular
            mechanisms by which PDCD5 can be used to treat cancers.
            </summary>
                <author>
                  <name>Changjing Zhuge</name>
                </author>
                <author>
                  <name>Xiaojuan Sun</name>
                </author>
                <author>
                  <name>Yingyu Chen</name>
                </author>
                <author>
                  <name>Jinzhi Lei</name>
                </author>
                <arxiv:comment xmlns:arxiv="http://arxiv.org/schemas/atom">12 pages, 7 figures</arxiv:comment>
                <link href="http://arxiv.org/abs/1503.08274v1" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/1503.08274v1" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
                <category term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
              <entry>
                <id>http://arxiv.org/abs/1510.04136v1</id>
                <updated>2015-10-14T15:05:09Z</updated>
                <published>2015-10-14T15:05:09Z</published>
                <title>Integral Control Feedback Circuit for the Reactivation of Malfunctioning
              p53 Pathway</title>
                <summary>  We developed a strategy to control p53 levels when the p53 concentration
            falls below a desired threshold . Based on an established p53-MDM2 mathematical
            model that also incorporates microRNA regulators (p53-MDM2-miRNA), we applied a
            control action in closed-loop with respect to this model. In particular, we
            employ various architectures of synthetic circuit with integral feedback
            control, illustrating varying degree in adaptive response to step-wise
            perturbation of p53.
            </summary>
                <author>
                  <name>Hsu Kiang Ooi</name>
                </author>
                <author>
                  <name>Lan Ma</name>
                </author>
                <arxiv:comment xmlns:arxiv="http://arxiv.org/schemas/atom">23 pages. 8 figures. Theoretical model of synthetic circuit that
              implements integral control feedback</arxiv:comment>
                <link href="http://arxiv.org/abs/1510.04136v1" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/1510.04136v1" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
                <category term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
                <category term="math.DS" scheme="http://arxiv.org/schemas/atom"/>
                <category term="math.OC" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
              <entry>
                <id>http://arxiv.org/abs/1503.08261v1</id>
                <updated>2015-03-28T02:47:10Z</updated>
                <published>2015-03-28T02:47:10Z</published>
                <title>Bifurcation analysis and potential landscape of the p53-Mdm2 oscillator
              regulated by the co-activator PDCD5</title>
                <summary>  Dynamics of p53 is known to play important roles in the regulation of cell
            fate decisions in response to various stresses, and PDCD5 functions as a
            co-activator of p53 to modulate the p53 dynamics. In the present paper, we
            investigate how p53 dynamics are modulated by PDCD5 during the DNA damage
            response using methods of bifurcation analysis and potential landscape. Our
            results reveal that p53 activities can display rich dynamics under different
            PDCD5 levels, including monostability, bistability with two stable steady
            states, oscillations, and co-existence of a stable steady state and an
            oscillatory state. Physical properties of the p53 oscillations are further
            shown by the potential landscape, in which the potential force attracts the
            system state to the limit cycle attractor, and the curl flux force drives the
            coherent oscillation along the cyclic. We also investigate the effect of PDCD5
            efficiency on inducing the p53 oscillations. We show that Hopf bifurcation is
            induced by increasing the PDCD5 efficiency, and the system dynamics show clear
            transition features in both barrier height and energy dissipation when the
            efficiency is close to the bifurcation point. This study provides a global
            picture of how PDCD5 regulates p53 dynamics via the interaction with the
            p53-Mdm2 oscillator and can be helpful in understanding the complicate p53
            dynamics in a more complete p53 pathway.
            </summary>
                <author>
                  <name>Yuanhong Bi</name>
                </author>
                <author>
                  <name>Zhuoqin Yang</name>
                </author>
                <author>
                  <name>Changjing Zhuge</name>
                </author>
                <author>
                  <name>Jinzhi Lei</name>
                </author>
                <arxiv:comment xmlns:arxiv="http://arxiv.org/schemas/atom">11 pages, 8 figures</arxiv:comment>
                <link href="http://arxiv.org/abs/1503.08261v1" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/1503.08261v1" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
                <category term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
              <entry>
                <id>http://arxiv.org/abs/1410.2990v1</id>
                <updated>2014-10-11T12:04:46Z</updated>
                <published>2014-10-11T12:04:46Z</published>
                <title>Bifurcation in cell cycle dynamics regulated by p53</title>
                <summary>  We study the regulating mechanism of p53 on the properties of cell cycle
            dynamics in the light of the proposed model of interacting p53 and cell cycle
            networks via p53. Irradiation (IR) introduce to p53 compel p53 dynamics to
            suffer different phases, namely oscillating and oscillation death (stabilized)
            phases. The IR induced p53 dynamics undergo collapse of oscillation with
            collapse time \Delta t which depends on IR strength. The stress p53 via IR
            drive cell cycle molecular species MPF and cyclin dynamics to different states,
            namely, oscillation death, oscillations of periods, chaotic and sustain
            oscillation in their bifurcation diagram. We predict that there could be a
            critical \Delta t_c induced by p53 via IR_c, where, if \Delta t &lt; \Delta t_c
            the cell cycle may come back to normal state, otherwise it will go to cell
            cycle arrest (apoptosis).
            </summary>
                <author>
                  <name>Md. Jahoor Alam</name>
                </author>
                <author>
                  <name>Sanjay Kumar</name>
                </author>
                <author>
                  <name>Vikram Singh</name>
                </author>
                <author>
                  <name>R. K. Brojen Singh</name>
                </author>
                <link href="http://arxiv.org/abs/1410.2990v1" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/1410.2990v1" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
                <category term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
              <entry>
                <id>http://arxiv.org/abs/q-bio/0510002v1</id>
                <updated>2005-10-01T00:11:33Z</updated>
                <published>2005-10-01T00:11:33Z</published>
                <title>A p53 Oscillator Model of DNA Break Repair Control</title>
                <summary>  The transcription factor p53 is an important regulator of cell fate.
            Mutations in p53 gene are associated with many cancers. In response to signals
            such as DNA damage, p53 controls the transcription of a series of genes that
            cause cell cycle arrest during which DNA damage is repaired, or triggers
            programmed cell death that eliminates possibly cancerous cells wherein DNA
            damage might have remained unrepaired. Previous experiments showed oscillations
            in p53 level in response to DNA damage, but the mechanism of oscillation
            remained unclear. Here we examine a model where the concentrations of p53
            isoforms are regulated by Mdm22, Arf, Siah, and beta-catenin. The extent of DNA
            damage is signalled through the switch-like activity of a DNA damage sensor,
            the DNA-dependent protein kinase Atm. This switch is responsible for initiating
            and terminating oscillations in p53 concentration. The strength of the DNA
            damage signal modulates the number of oscillatory waves of p53 and Mdm22 but
            not the frequency or amplitude of oscillations{a result that recapitulates
            experimental findings. A critical fnding was that the phosphorylated form of
            Nbs11, a member of the DNA break repair complex Mre11-Rad50-Nbs11 (MRN), must
            augment the activity of Atm kinase. While there is in vitro support for this
            assumption, this activity appears essential for p53 dynamics. The model
            provides several predictions concerning, among others, degradation of the
            phosphorylated form of p53, the rate of DNA repair as a function of DNA damage,
            the sensitivity of p53 oscillation to transcription rates of SIAH, beta-CATENIN
            and ARF, and the hysteretic behavior of active Atm kinase levels with respect
            to the DNA damage signal
            </summary>
                <author>
                  <name>Vijay Chickarmane</name>
                </author>
                <author>
                  <name>Ali Nadim</name>
                </author>
                <author>
                  <name>Animesh Ray</name>
                </author>
                <author>
                  <name>Herbert M. Sauro</name>
                </author>
                <arxiv:comment xmlns:arxiv="http://arxiv.org/schemas/atom">31 pages, 8 figures</arxiv:comment>
                <link href="http://arxiv.org/abs/q-bio/0510002v1" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/q-bio/0510002v1" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
                <category term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
              <entry>
                <id>http://arxiv.org/abs/0708.3171v1</id>
                <updated>2007-08-23T14:36:43Z</updated>
                <published>2007-08-23T14:36:43Z</published>
                <title>The p53-MDM2 network: from oscillations to apoptosis</title>
                <summary>  The p53 protein is well-known for its tumour suppressor function. The
            p53-MDM2 negative feedback loop constitutes the core module of a network of
            regulatory interactions activated under cellular stress. In normal cells, the
            level of p53 proteins is kept low by MDM2, i.e. MDM2 negatively regulates the
            activity of p53. In the case of DNA damage,the p53-mediated pathways are
            activated leading to cell cycle arrest and repair of the DNA. If repair is not
            possible due to excessive damage, the p53-mediated apoptotic pathway is
            activated bringing about cell death. In this paper, we give an overview of our
            studies on the p53-MDM2 module and the associated pathways from a systems
            biology perspective. We discuss a number of key predictions, related to some
            specific aspects of cell cycle arrest and cell death, which could be tested in
            experiments.
            </summary>
                <author>
                  <name>Indrani Bose</name>
                </author>
                <author>
                  <name>Bhaswar Ghosh</name>
                </author>
                <link href="http://arxiv.org/abs/0708.3171v1" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/0708.3171v1" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
                <category term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
              <entry>
                <id>http://arxiv.org/abs/1701.04967v1</id>
                <updated>2017-01-18T06:48:31Z</updated>
                <published>2017-01-18T06:48:31Z</published>
                <title>Structural Effects and Competition Mechanisms Targeting the Interactions
              between p53 and Mdm2 for Cancer Therapy</title>
                <summary>  About half of human cancers show normal TP53 gene and aberrant overexpression
            of Mdm2 and/or MdmX. This fact promotes a promising cancer therapeutic strategy
            which targeting the interactions between p53 and Mdm2/MdmX. For developing the
            inhibitors to disrupt the p53-Mdm2/MdmX interactions, we systematically
            investigate structural and interaction characteristics of p53 and inhibitors
            with Mdm2 and MdmX from atomistic level by exploiting stochastic molecular
            dynamics simulations. We find that some specific $\alpha$ helices in Mdm2 and
            MdmX structure play key role in their bindings with inhibitors and the hydrogen
            bond formed by residue Trp23 of p53 with its counterpart in Mdm2/MdmX
            determines dynamical competition processes of the disruption of Mdm2-p53
            interaction and replacement of p53 from Mdm2-p53 complex {\it in vivo}. We hope
            that the results reported in this paper provide basic information for designing
            functional inhibitors and realizing cancer gene therapy.
            </summary>
                <author>
                  <name>Shuxia Liu</name>
                </author>
                <author>
                  <name>Yizhao Geng</name>
                </author>
                <author>
                  <name>Shiwei Yan</name>
                </author>
                <arxiv:comment xmlns:arxiv="http://arxiv.org/schemas/atom">9 pages, 8 figures</arxiv:comment>
                <link href="http://arxiv.org/abs/1701.04967v1" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/1701.04967v1" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
                <category term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
                <category term="physics.bio-ph" scheme="http://arxiv.org/schemas/atom"/>
                <category term="q-bio.BM" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
              <entry>
                <id>http://arxiv.org/abs/1409.1943v1</id>
                <updated>2014-09-01T11:39:42Z</updated>
                <published>2014-09-01T11:39:42Z</published>
                <title>The co-existence of states in p53 dynamics driven by miRNA</title>
                <summary>  The regulating mechanism of miRNA on p53 dynamics in p53-MDM2-miRNA model
            network incorporating reactive oxygen species (ROS) is studied. The study shows
            that miRNA drives p53 dynamics at various states, namely, stabilized states and
            oscillating states (damped and sustain oscillation). We found the co-existence
            of these states within certain range of the concentartion level of miRNA in the
            system. This co-existence in p53 dynamics is the signature of the system's
            survival at various states, normal, activated and apoptosis driven by a
            constant concentration of miRNA.
            </summary>
                <author>
                  <name>Md. Jahoor Alam</name>
                </author>
                <author>
                  <name>Shazia Kunvar</name>
                </author>
                <author>
                  <name>R. K. Brojen Singh</name>
                </author>
                <link href="http://arxiv.org/abs/1409.1943v1" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/1409.1943v1" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
                <category term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
              <entry>
                <id>http://arxiv.org/abs/1211.5073v1</id>
                <updated>2012-11-21T16:18:30Z</updated>
                <published>2012-11-21T16:18:30Z</published>
                <title>On the role of intrinsic noise on the response of the p53-Mdm2 module</title>
                <summary>  The protein p53 has a well established role in protecting genomic integrity
            in human cells. When DNA is damaged p53 induces the cell cycle arrest to
            prevent the transmission of the damage to cell progeny, triggers the production
            of proteins for DNA repair and ultimately calls for apoptosis. In particular,
            the p53-Mdm2 feedback loop seems to be the key circuit in this response of
            cells to damage. For many years, based on measurements over populations of
            cells it was believed that the p53-Mdm2 feedback loop was the responsible for
            the existence of damped oscillations in the levels of p53 and Mdm2 after DNA
            damage. However, recent measurements in individual human cells have shown that
            p53 and its regulator Mdm2 develop sustained oscillations over long periods of
            time even in the absence of stress. These results have attracted a lot of
            interest, first because they open a new experimental framework to study the p53
            and its interactions and second because they challenge years of mathematical
            models with new and accurate data on single cells. Inspired by these
            experiments standard models of the p53-Mdm2 circuit were modified introducing
            ad-hoc some biologically motivated noise that becomes responsible for the
            stability of the oscillations. Here, we follow an alternative approach
            proposing that the noise that stabilizes the fluctuations is the intrinsic
            noise due to the finite nature of the populations of p53 and Mdm2 in a single
            cell.
            </summary>
                <author>
                  <name>Lídice Cruz-Rodríguez</name>
                </author>
                <author>
                  <name>Nuris Figueroa-Morales</name>
                </author>
                <author>
                  <name>Roberto Mulet</name>
                </author>
                <arxiv:comment xmlns:arxiv="http://arxiv.org/schemas/atom">10 pages, 9 figures</arxiv:comment>
                <link href="http://arxiv.org/abs/1211.5073v1" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/1211.5073v1" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
                <category term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
                <category term="cond-mat.soft" scheme="http://arxiv.org/schemas/atom"/>
                <category term="physics.bio-ph" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
              <entry>
                <id>http://arxiv.org/abs/1109.0743v1</id>
                <updated>2011-09-04T19:29:19Z</updated>
                <published>2011-09-04T19:29:19Z</published>
                <title>Stochastic modeling of p53-regulated apoptosis upon radiation damage</title>
                <summary>  We develop and study the evolution of a model of radiation induced apoptosis
            in cells using stochastic simulations, and identified key protein targets for
            effective mitigation of radiation damage. We identified several key proteins
            associated with cellular apoptosis using an extensive literature survey. In
            particular, we focus on the p53 transcription dependent and p53 transcription
            independent pathways for mitochondrial apoptosis. Our model reproduces known
            p53 oscillations following radiation damage. The key, experimentally testable
            hypotheses that we generate are - inhibition of PUMA is an effective strategy
            for mitigation of radiation damage if the treatment is administered
            immediately, at later stages following radiation damage, inhibition of tBid is
            more effective.
            </summary>
                <author>
                  <name>Divesh Bhatt</name>
                </author>
                <author>
                  <name>Zoltan Oltvai</name>
                </author>
                <author>
                  <name>Ivet Bahar</name>
                </author>
                <link href="http://arxiv.org/abs/1109.0743v1" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/1109.0743v1" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="physics.bio-ph" scheme="http://arxiv.org/schemas/atom"/>
                <category term="physics.bio-ph" scheme="http://arxiv.org/schemas/atom"/>
                <category term="q-bio.MN" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
            </feed>
            */


            /*
            value = ArXiv Query: search_query=all:p53&id_list=&start=0&max_results=10
            length = 10
            length = 14
            http://arxiv.org/abs/1503.08274v1
            28/03/2015 7:05:10
            28/03/2015 7:05:10
            PDCD5 interacts with p53 and functions as a regulator of p53 dynamics in
              the DNA damage response
              The tumor suppressor p53 plays a central role in cell fate decisions after
            DNA damage. Programmed Cell Death 5 (PDCD5) is known to interact with the p53
            pathway to promote cell apoptosis. Recombinant human PDCD5 can significantly
            sensitize different cancers to chemotherapies. In the present paper, we
            construct a computational model that includes PDCD5 interactions in the p53
            signaling network and study the effects of PDCD5 on p53-mediated cell fate
            decisions during the DNA damage response. Our results revealed that PDCD5
            functions as a co-activator of p53 that regulates p53-dependent cell fate
            decisions via the mediation of p53 dynamics. The effects of PDCD5 are
            dose-dependent such that p53 can display either sustained or pulsed dynamics at
            different PDCD5 levels. Moreover, PDCD5 regulates caspase-3 activation via two
            mechanisms during the two phases of sustained and pulsed p53 dynamics. This
            study provides insights regarding how PDCD5 functions as a regulator of the p53
            pathway and might be helpful for increasing our understanding of the molecular
            mechanisms by which PDCD5 can be used to treat cancers.

            12 pages, 7 figures
            length = 14
            http://arxiv.org/abs/1510.04136v1
            14/10/2015 15:05:09
            14/10/2015 15:05:09
            Integral Control Feedback Circuit for the Reactivation of Malfunctioning
              p53 Pathway
              We developed a strategy to control p53 levels when the p53 concentration
            falls below a desired threshold . Based on an established p53-MDM2 mathematical
            model that also incorporates microRNA regulators (p53-MDM2-miRNA), we applied a
            control action in closed-loop with respect to this model. In particular, we
            employ various architectures of synthetic circuit with integral feedback
            control, illustrating varying degree in adaptive response to step-wise
            perturbation of p53.

            23 pages. 8 figures. Theoretical model of synthetic circuit that
              implements integral control feedback
            length = 14
            http://arxiv.org/abs/1503.08261v1
            28/03/2015 2:47:10
            28/03/2015 2:47:10
            Bifurcation analysis and potential landscape of the p53-Mdm2 oscillator
              regulated by the co-activator PDCD5
              Dynamics of p53 is known to play important roles in the regulation of cell
            fate decisions in response to various stresses, and PDCD5 functions as a
            co-activator of p53 to modulate the p53 dynamics. In the present paper, we
            investigate how p53 dynamics are modulated by PDCD5 during the DNA damage
            response using methods of bifurcation analysis and potential landscape. Our
            results reveal that p53 activities can display rich dynamics under different
            PDCD5 levels, including monostability, bistability with two stable steady
            states, oscillations, and co-existence of a stable steady state and an
            oscillatory state. Physical properties of the p53 oscillations are further
            shown by the potential landscape, in which the potential force attracts the
            system state to the limit cycle attractor, and the curl flux force drives the
            coherent oscillation along the cyclic. We also investigate the effect of PDCD5
            efficiency on inducing the p53 oscillations. We show that Hopf bifurcation is
            induced by increasing the PDCD5 efficiency, and the system dynamics show clear
            transition features in both barrier height and energy dissipation when the
            efficiency is close to the bifurcation point. This study provides a global
            picture of how PDCD5 regulates p53 dynamics via the interaction with the
            p53-Mdm2 oscillator and can be helpful in understanding the complicate p53
            dynamics in a more complete p53 pathway.

            11 pages, 8 figures
            length = 13
            http://arxiv.org/abs/1410.2990v1
            11/10/2014 12:04:46
            11/10/2014 12:04:46
            Bifurcation in cell cycle dynamics regulated by p53
              We study the regulating mechanism of p53 on the properties of cell cycle
            dynamics in the light of the proposed model of interacting p53 and cell cycle
            networks via p53. Irradiation (IR) introduce to p53 compel p53 dynamics to
            suffer different phases, namely oscillating and oscillation death (stabilized)
            phases. The IR induced p53 dynamics undergo collapse of oscillation with
            collapse time \Delta t which depends on IR strength. The stress p53 via IR
            drive cell cycle molecular species MPF and cyclin dynamics to different states,
            namely, oscillation death, oscillations of periods, chaotic and sustain
            oscillation in their bifurcation diagram. We predict that there could be a
            critical \Delta t_c induced by p53 via IR_c, where, if \Delta t < \Delta t_c
            the cell cycle may come back to normal state, otherwise it will go to cell
            cycle arrest (apoptosis).

            length = 14
            http://arxiv.org/abs/q-bio/0510002v1
            1/10/2005 0:11:33
            1/10/2005 0:11:33
            A p53 Oscillator Model of DNA Break Repair Control
              The transcription factor p53 is an important regulator of cell fate.
            Mutations in p53 gene are associated with many cancers. In response to signals
            such as DNA damage, p53 controls the transcription of a series of genes that
            cause cell cycle arrest during which DNA damage is repaired, or triggers
            programmed cell death that eliminates possibly cancerous cells wherein DNA
            damage might have remained unrepaired. Previous experiments showed oscillations
            in p53 level in response to DNA damage, but the mechanism of oscillation
            remained unclear. Here we examine a model where the concentrations of p53
            isoforms are regulated by Mdm22, Arf, Siah, and beta-catenin. The extent of DNA
            damage is signalled through the switch-like activity of a DNA damage sensor,
            the DNA-dependent protein kinase Atm. This switch is responsible for initiating
            and terminating oscillations in p53 concentration. The strength of the DNA
            damage signal modulates the number of oscillatory waves of p53 and Mdm22 but
            not the frequency or amplitude of oscillations{a result that recapitulates
            experimental findings. A critical fnding was that the phosphorylated form of
            Nbs11, a member of the DNA break repair complex Mre11-Rad50-Nbs11 (MRN), must
            augment the activity of Atm kinase. While there is in vitro support for this
            assumption, this activity appears essential for p53 dynamics. The model
            provides several predictions concerning, among others, degradation of the
            phosphorylated form of p53, the rate of DNA repair as a function of DNA damage,
            the sensitivity of p53 oscillation to transcription rates of SIAH, beta-CATENIN
            and ARF, and the hysteretic behavior of active Atm kinase levels with respect
            to the DNA damage signal

            31 pages, 8 figures
            length = 11
            http://arxiv.org/abs/0708.3171v1
            23/08/2007 14:36:43
            23/08/2007 14:36:43
            The p53-MDM2 network: from oscillations to apoptosis
              The p53 protein is well-known for its tumour suppressor function. The
            p53-MDM2 negative feedback loop constitutes the core module of a network of
            regulatory interactions activated under cellular stress. In normal cells, the
            level of p53 proteins is kept low by MDM2, i.e. MDM2 negatively regulates the
            activity of p53. In the case of DNA damage,the p53-mediated pathways are
            activated leading to cell cycle arrest and repair of the DNA. If repair is not
            possible due to excessive damage, the p53-mediated apoptotic pathway is
            activated bringing about cell death. In this paper, we give an overview of our
            studies on the p53-MDM2 module and the associated pathways from a systems
            biology perspective. We discuss a number of key predictions, related to some
            specific aspects of cell cycle arrest and cell death, which could be tested in
            experiments.

            length = 15
            http://arxiv.org/abs/1701.04967v1
            18/01/2017 6:48:31
            18/01/2017 6:48:31
            Structural Effects and Competition Mechanisms Targeting the Interactions
              between p53 and Mdm2 for Cancer Therapy
              About half of human cancers show normal TP53 gene and aberrant overexpression
            of Mdm2 and/or MdmX. This fact promotes a promising cancer therapeutic strategy
            which targeting the interactions between p53 and Mdm2/MdmX. For developing the
            inhibitors to disrupt the p53-Mdm2/MdmX interactions, we systematically
            investigate structural and interaction characteristics of p53 and inhibitors
            with Mdm2 and MdmX from atomistic level by exploiting stochastic molecular
            dynamics simulations. We find that some specific $\alpha$ helices in Mdm2 and
            MdmX structure play key role in their bindings with inhibitors and the hydrogen
            bond formed by residue Trp23 of p53 with its counterpart in Mdm2/MdmX
            determines dynamical competition processes of the disruption of Mdm2-p53
            interaction and replacement of p53 from Mdm2-p53 complex {\it in vivo}. We hope
            that the results reported in this paper provide basic information for designing
            functional inhibitors and realizing cancer gene therapy.

            9 pages, 8 figures
            length = 12
            http://arxiv.org/abs/1409.1943v1
            1/09/2014 11:39:42
            1/09/2014 11:39:42
            The co-existence of states in p53 dynamics driven by miRNA
              The regulating mechanism of miRNA on p53 dynamics in p53-MDM2-miRNA model
            network incorporating reactive oxygen species (ROS) is studied. The study shows
            that miRNA drives p53 dynamics at various states, namely, stabilized states and
            oscillating states (damped and sustain oscillation). We found the co-existence
            of these states within certain range of the concentartion level of miRNA in the
            system. This co-existence in p53 dynamics is the signature of the system's
            survival at various states, normal, activated and apoptosis driven by a
            constant concentration of miRNA.

            length = 15
            http://arxiv.org/abs/1211.5073v1
            21/11/2012 16:18:30
            21/11/2012 16:18:30
            On the role of intrinsic noise on the response of the p53-Mdm2 module
              The protein p53 has a well established role in protecting genomic integrity
            in human cells. When DNA is damaged p53 induces the cell cycle arrest to
            prevent the transmission of the damage to cell progeny, triggers the production
            of proteins for DNA repair and ultimately calls for apoptosis. In particular,
            the p53-Mdm2 feedback loop seems to be the key circuit in this response of
            cells to damage. For many years, based on measurements over populations of
            cells it was believed that the p53-Mdm2 feedback loop was the responsible for
            the existence of damped oscillations in the levels of p53 and Mdm2 after DNA
            damage. However, recent measurements in individual human cells have shown that
            p53 and its regulator Mdm2 develop sustained oscillations over long periods of
            time even in the absence of stress. These results have attracted a lot of
            interest, first because they open a new experimental framework to study the p53
            and its interactions and second because they challenge years of mathematical
            models with new and accurate data on single cells. Inspired by these
            experiments standard models of the p53-Mdm2 circuit were modified introducing
            ad-hoc some biologically motivated noise that becomes responsible for the
            stability of the oscillations. Here, we follow an alternative approach
            proposing that the noise that stabilizes the fluctuations is the intrinsic
            noise due to the finite nature of the populations of p53 and Mdm2 in a single
            cell.

            10 pages, 9 figures
            length = 13
            http://arxiv.org/abs/1109.0743v1
            4/09/2011 19:29:19
            4/09/2011 19:29:19
            Stochastic modeling of p53-regulated apoptosis upon radiation damage
              We develop and study the evolution of a model of radiation induced apoptosis
            in cells using stochastic simulations, and identified key protein targets for
            effective mitigation of radiation damage. We identified several key proteins
            associated with cellular apoptosis using an extensive literature survey. In
            particular, we focus on the p53 transcription dependent and p53 transcription
            independent pathways for mitochondrial apoptosis. Our model reproduces known
            p53 oscillations following radiation damage. The key, experimentally testable
            hypotheses that we generate are - inhibition of PUMA is an effective strategy
            for mitigation of radiation damage if the treatment is administered
            immediately, at later stages following radiation damage, inhibition of tBid is
            more effective.
            */


            Console.Read();
        }
    }
}
