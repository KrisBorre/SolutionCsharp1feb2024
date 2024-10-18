using LibraryArxiv24may2024;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ConsoleArxiv24may2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Arxiv database!");

            ArxivDbContext23may2024 dbContext = new ArxivDbContext23may2024();
            dbContext.Database.EnsureCreated();

            string word1 = "adiabatic";
            string word2 = "invariant";
            string word3 = "elliptic";

            List<Article> allArticles = dbContext.Articles
                .Include(a => a.Contributions)
                .Include(a => a.Links)
                .ToList();

            Console.WriteLine("Number of Articles: " + allArticles.Count); // 2445

            List<Contribution> allContributions = dbContext.Contributions.ToList();
            List<Link> allLinks = dbContext.Links.ToList();

            var selectedArticles1 = allArticles.FindAll(x => x.Title.Contains(word1, StringComparison.OrdinalIgnoreCase));  // 126

            var selectedArticles2 = allArticles.FindAll(x => x.Title.Contains(word2, StringComparison.OrdinalIgnoreCase));  // 67

            var bothArticles1 = allArticles.FindAll(x => (x.Title.Contains(word1, StringComparison.OrdinalIgnoreCase) && x.Title.Contains(word2, StringComparison.OrdinalIgnoreCase))); // 20

            //foreach (var article in bothArticles1)
            //{
            //    string[] titles = article.Title.Split("\n");
            //    foreach (var t in titles)
            //    {
            //        Console.WriteLine(t);
            //    }
            //    Console.WriteLine();
            //    //Console.WriteLine(article.Abstract + "\n");
            //    foreach (var link in article.Links)
            //    {
            //        if (link.Hyperlink.Contains("pdf"))
            //        {
            //            Console.WriteLine(link.Hyperlink);
            //        }
            //    }
            //    Console.WriteLine("\n");
            //}

            /*
            Invariant Perturbation Theory of Adiabatic Process

            http://arxiv.org/pdf/0706.0299v3


            Adiabatic invariants for the dynamics of rotating 3-dimensional fluid

            http://arxiv.org/pdf/1206.1018v1


            General formulas for adiabatic invariants in nearly-periodic Hamiltonian
              systems

            http://arxiv.org/pdf/2005.00634v1


            On adiabatic invariant in generalized Galileon theories

            http://arxiv.org/pdf/1505.04670v1


            On the interplay between Noether's theorem and the theory of adiabatic
              invariants

            http://arxiv.org/pdf/2012.08853v1


            Adiabatic invariants drive rhythmic human motion in variable gravity

            http://arxiv.org/pdf/1906.08686v1


            The Rayleigh-Lorentz Invariant and Optimal Adiabatic Qubit-Information
              Detection for Superconducting Qubit Resonators

            http://arxiv.org/pdf/2007.10671v1


            Robust stimulated Raman shortcut-to-adiabatic passage by invariant-based
              optimal control

            http://arxiv.org/pdf/2103.01386v1


            On the accuracy of conservation of adiabatic invariants in slow-fast
              systems

            http://arxiv.org/pdf/1103.1595v1


            Change in the adiabatic invariant in a nonlinear Landau-Zener problem

            http://arxiv.org/pdf/0910.3061v2


            Borel summation of adiabatic invariants

            http://arxiv.org/pdf/math/0608315v1


            Adiabatic invariants of the extended KdV equation

            http://arxiv.org/pdf/1512.01194v3


            Dynamical invariant formalism of shortcuts to adiabaticity

            http://arxiv.org/pdf/2209.04367v1


            Exact Analysis of the Adiabatic Invariants in Time-Dependent Harmonic
              Oscillator

            http://arxiv.org/pdf/nlin/0506033v1


            The invariant based investigation of Shortcut to Adiabaticity for
              Quantum Harmonic Oscillators under time varying frictional force

            http://arxiv.org/pdf/1910.04575v2


            Higher-derivative harmonic oscillators: stability of classical dynamics
              and adiabatic invariants

            http://arxiv.org/pdf/1811.07733v2


            On the number of independent adiabatic invariants for gyrating particles

            http://arxiv.org/pdf/0707.2471v1


            Adiabatic limit of the eta invariant over cofinite quotient of PSL(2,R)

            http://arxiv.org/pdf/0705.4506v1


            Adiabatic invariants for the regular region of the Dicke model

            http://arxiv.org/pdf/1611.07863v1


            Change in the adiabatic invariant in a nonlinear two-mode model of
              Feshbach resonance passage

            http://arxiv.org/pdf/nlin/0611007v2
            */

            var bothArticles2 = allArticles.FindAll(x => ((x.Title.Contains(word1, StringComparison.OrdinalIgnoreCase) || x.Abstract.Contains(word1, StringComparison.OrdinalIgnoreCase)) && (x.Title.Contains(word2, StringComparison.OrdinalIgnoreCase) || x.Abstract.Contains(word2, StringComparison.OrdinalIgnoreCase)))); // 97

            var threeArticles1 = bothArticles2.FindAll(x => x.Abstract.Contains(word3, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine("The number of articles about adiabatic invariants and elliptic ... : " + threeArticles1.Count); // 8

            foreach (var article in threeArticles1)
            {
                string[] titles = article.Title.Split("\n");
                foreach (var t in titles)
                {
                    Console.WriteLine(t);
                }
                Console.WriteLine();
                //Console.WriteLine(article.Abstract + "\n");
                foreach (var link in article.Links)
                {
                    if (link.Hyperlink.Contains("pdf"))
                    {
                        Console.WriteLine(link.Hyperlink);
                    }
                }
                Console.WriteLine("\n");
            }

            /*
            Dicke model semiclassical dynamics in superradiant dipolar phase in the 'bound luminosity' state
            http://arxiv.org/pdf/2309.12446v1
            */

            var grouped = allContributions.GroupBy(c => c.Author);

            foreach (var group in grouped)
            {
                if (group.Count() > 6) Console.WriteLine($"{group.Key} ({group.Count()})");
            }
            /*
            G. Sardanashvily (8)
            Alain J. Brizard (11)
            A. I. Neishtadt (8)
            Jérôme Darmont (16)
            Fadila Bentayeb (7)
            T. Mart (10)
            James Atwood (7)
            */

            #region announcement date and submission date

            int earlier = allArticles.FindAll(a => a.DateTime1 < a.DateTime2).Count;
            Console.WriteLine("earlier = " + earlier);
            int same = allArticles.FindAll(a => a.DateTime1 == a.DateTime2).Count;
            Console.WriteLine("same = " + same);
            int later = allArticles.FindAll(a => a.DateTime1 > a.DateTime2).Count; //submission date? // announcement date?
            Console.WriteLine("later = " + later);

            /*
            earlier = 0
            same = 566
            later = 199
            */
            #endregion

            #region github
            List<Article> githubArticles = allArticles.FindAll(a => a.Abstract.Contains("github"));

            foreach (var article in githubArticles)
            {
                string[] titles = article.Title.Split("\n");
                foreach (var t in titles)
                {
                    Console.Write(t);
                }
                Console.WriteLine();

                //Console.WriteLine(article.Abstract + "\n");

                string pattern = @"https://github.com/.*";
                Match match = Regex.Match(article.Abstract, pattern);

                if (match.Success)
                {
                    string url = match.Groups[0].Value.TrimEnd('.', '}', ')', ',');
                    Console.WriteLine(url);
                }

                Console.WriteLine();
            }

            /*         
            WW-Nets: Dual Neural Networks for Object Detection
            https://github.com/mkebrahimpour
            Python

            DGoT: Dynamic Graph of Thoughts for Scientific Abstract Generation
            https://github.com/JayceNing/DGoT
            Python

            Large Language Models are Fixated by Red Herrings: Exploring Creative
            https://github.com/TaatiTeam/OCW
            Python
            
            Joint Learning of Salient Object Detection, Depth Estimation and Contour
            https://github.com/Xiaoqi-Zhao-DLUT/MMFT
            Python

            VISLA Benchmark: Evaluating Embedding Sensitivity to Semantic and
            https://github.com/Sri-Harsha/visla_benchmark
            no code 

            MORE: Simultaneous Multi-View 3D Object Recognition and Pose Estimation
            https://github.com/SubhadityaMukherjee/more_mvcnn
            Python

            Putting visual object recognition in context
            https://github.com/kreimanlab/Put-In-Context
            Matlab

            Unsupervised Domain Attention Adaptation Network for Caricature
            https://github.com/KeleiHe/DAAN
            Python

            Genome-on-Diet: Taming Large-Scale Genomic Analyses via Sparsified
            https://github.com/CMU-SAFARI/Genome-on-Diet
            Roff

            Identifying recombination hotspots using population genetic data
            http://github.com/auton1/LDhot
            C++

            MATES: Model-Aware Data Selection for Efficient Pretraining with Data
            https://github.com/cxcscmu/MATES
            Python

            Fin-Fact: A Benchmark Dataset for Multimodal Financial Fact Checking and
            https://github.com/IIT-DM/Fin-Fact/
            Python

            Label-Efficient Learning in Agriculture: A Comprehensive Review
            https://github.com/DongChen06/Label-efficient-in-Agriculture
            Python

            TorchGeo: Deep Learning With Geospatial Data
            https://github.com/microsoft/torchgeo
            Python

            FlightScope: A Deep Comprehensive Assessment of Aircraft Detection
            https://github.com/toelt-llc/FlightScope_Bench
            Python Jupyter Notebook 

            Public Computer Vision Datasets for Precision Livestock Farming: A
            https://github.com/Anil-Bhujel/Public-Computer-Vision-Dataset-A-Systematic-Survey
            no code

            Landmark Detection and 3D Face Reconstruction for Caricature using a
            https://github.com/Juyong/CaricatureFace
            Python

            Weakly-supervised Caricature Face Parsing through Domain Adaptation
            https://github.com/ZJULearning/CariFaceParsing
            Python

            Self-supervised transformer-based pre-training method with General Plant
            https://github.com/WASSER2545/GPID-22
            not found

            Component-aware anomaly detection framework for adjustable and logical
            https://github.com/liutongkun/ComAD
            Python

            Cross-Domain Graph Anomaly Detection via Anomaly-aware Contrastive
            https://github.com/QZ-WANG/ACT
            Python

            RealNet: A Feature Selection Network with Realistic Synthetic Anomaly
            https://github.com/cnulab/RealNet
            Python

            Towards Scalable 3D Anomaly Detection and Localization: A Benchmark via
            https://github.com/Chopper-233/Anomaly-ShapeNet
            no code

            Exploiting Structural Consistency of Chest Anatomy for Unsupervised
            https://github.com/MrGiovanni/SimSID
            Python

            Exploring CLIP for Assessing the Look and Feel of Images
            https://github.com/IceClear/CLIP-IQA
            Python

            Universal Instance Perception as Object Discovery and Retrieval
            https://github.com/MasterBin-IIAU/UNINEXT
            Python

            PreprintMatch: a tool for preprint publication detection applied to
            https://github.com/PeterEckmann1/preprint-match
            Python

            Image-to-Image Translation via Group-wise Deep Whitening-and-Coloring
            https://github.com/WonwoongCho/GDWCT
            Python
            indrukwekkend

            Online Analytic Exemplar-Free Continual Learning with Large Models for
            https://github.com/ZHUANGHP/Analytic-continual-learning
            Python

            ScrewNet: Category-Independent Articulation Model Estimation From Depth
            https://pearl-utexas.github.io/ScrewNet/
            Python

            Distributional Depth-Based Estimation of Object Articulation Models
            https://pearl-utexas.github.io/DUST-net/
            Python

            WordStylist: Styled Verbatim Handwritten Text Generation with Latent
            https://github.com/koninik/WordStylist
            Python

            GR-RNN: Global-Context Residual Recurrent Neural Networks for Writer
            https://github.com/shengfly/writer-identification
            Python

            EmoGen: Eliminating Subjective Bias in Emotional Music Generation
            https://ai-muzic.github.io/emogen/
            https://github.com/microsoft/muzic/
            Python
            heel indrukwekkend

            Using Emotion Embeddings to Transfer Knowledge Between Emotions
            https://github.com/gchochla/Demux-MEmo
            Python

            EmoVIT: Revolutionizing Emotion Insights with Visual Instruction Tuning
            https://github.com/aimmemotion/EmoVIT
            Python

            DRT: A Lightweight Single Image Deraining Recursive Transformer
            https://github.com/YC-Liang/DRT
            Python

            Real-time Transformer-based Open-Vocabulary Detection with Efficient
            https://github.com/om-ai-lab/OmDet
            Python

            Data-Efficient Unsupervised Interpolation Without Any Intermediate Frame
            https://github.com/jungeun122333/UVI-Net
            Python

            Learning to Localize Actions from Moments
            https://github.com/FuchenUSTC/AherNet
            Python
            */
            #endregion

            Console.WriteLine();

            #region abstract uses the words from the title
            //Console.WriteLine("\n\nLet's check if the abstract and the title use the same words.");
            //int number_of_articles = 0;
            //foreach (Article article in allArticles)
            //{
            //    string[] array = article.Title.Split(' ');
            //    int used = 0;

            //    foreach (var titleWord in array)
            //    {
            //        if (article.Abstract.Contains(titleWord, StringComparison.InvariantCultureIgnoreCase))
            //        {
            //            used++;
            //        }
            //    }

            //    double ratio = used / ((double)array.Length);

            //    if (ratio == 1.0)
            //    {
            //        Console.WriteLine(article.Title);
            //        number_of_articles++;
            //    }
            //}

            //Console.WriteLine(number_of_articles / ((double)allArticles.Count));
            //// 1.0 -> 0,199 // 0.95 -> 0,201 // 0.90 -> 0,271 // 0.85 -> 0,415 // 0.80 -> 0,511        

            //Console.WriteLine("\n20 percent of abstracts contain all the title words.");

            //Console.WriteLine("27 percent of abstracts contain 90 percent of the title words.");

            //Console.WriteLine("41 percent of abstracts contain 85 percent of the title words.");

            //Console.WriteLine("51 percent of abstracts contain 80 percent of the title words.\n");
            #endregion

            #region abstracts usually don't use references

            //int brackets = 0;

            //foreach (Article article in allArticles)
            //{
            //    if ((article.Abstract.Contains("(1998") || article.Abstract.Contains("(201")) && article.Abstract.Contains(')'))
            //    {
            //        brackets++;
            //        Console.WriteLine(article.Abstract);
            //    }
            //}

            //Console.WriteLine(brackets / ((double)allArticles.Count));

            /*
            We present the second-order phase transition from a band insulator to metal
            that is induced by a strong magnetic field. The magnetic-field dependences of
            the magnetization and energy band gap of a crystalline silicon immersed in a
            magnetic field are investigated by means of the nonperturbative
            magnetic-field-containing relativistic tight-binding approximation method
            [Phys. Rev. B 97, 195135 (2018)].

            This paper discusses asymptotic theory for penalized spline estimators in
            generalized additive models. The purpose of this paper is to establish the
            asymptotic bias and variance as well as the asymptotic normality of the
            penalized spline estimators proposed by Marx and Eilers (1998).

            A time dependence of a metal abundance of supernova remnant (SNR) have been
            found by Hughes et al. (1998).

            The time-dependent surface flux method developed for the description of
            electronic spectra [L. Tao and A. Scrinzi, New J. Phys. 14, 013021 (2012); A.
            Scrinzi, New J. Phys. 14, 085008 (2012)] is extended to treat dissociation and
            dissociative ionization processes of H2+ interacting with strong laser pulses.

            A range of a priori hypotheses about the evolution of modern and archaic
            genomes are further evaluated and tested. In addition to the well-known
            splits/introgressions involving Neanderthal genes into out-of- Africa people,
            or Denisovan genes into Oceanians, a further series of archaic splits and
            hypotheses proposed in Waddell et al. (2011) are considered in detail.

            In a recent paper, Hirsch (2018) proposes to attribute the credit for a
            co-authored paper to the {\alpha}-author--the author with the highest
            h-index--regardless of his or her actual contribution, effectively reducing the
            role of the other co-authors to zero.

            Dispersal of species to find a more favorable habitat is important in
            population dynamics. Dispersal rates evolve in response to the relative success
            of different dispersal strategies. In a simplified deterministic treatment (J.
            Dockery, V. Hutson, K. Mischaikow, et al., J. Math. Bio. 37, 61 (1998)) of two
            species which differ only in their dispersal rates the slow species always
            dominates.

            It has recently been shown that a parametrically driven oscillator with Kerr
            nonlinearity yields a Schr\"odinger cat state via quantum adiabatic evolution
            through its bifurcation point and a network of such nonlinear oscillators can
            be used for solving combinatorial optimization problems by bifurcation-based
            adiabatic quantum computation [H. Goto, Sci. Rep. \textbf{6}, 21686 (2016)].
            */

            //Console.WriteLine("\nOnly 1 percent of abstracts use references.");
            //Console.WriteLine("99 percent of abstracts use no references.");
            #endregion

            #region agricultur and crop
            //List<Article> agriculture = allArticles
            //    .Where(a => a.Title.Contains("agricultur", StringComparison.OrdinalIgnoreCase) ||
            //                a.Title.Contains(" crop ", StringComparison.OrdinalIgnoreCase))
            //    .ToList();

            //foreach (Article article in agriculture) // 41 articles
            //{
            //    string title = article.Title.ReplaceLineEndings(" ");
            //    Console.WriteLine(title);
            //    Link? link = article.Links.Where(l => l.Hyperlink.Contains("pdf", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            //    Console.WriteLine(link?.Hyperlink);
            //}
            #endregion

            #region questions
            List<Article> questionsInAbstractAndTitle = new List<Article>();
            List<Article> questionsInAbstract = new List<Article>();
            List<Article> questionsInTitle = new List<Article>();

            foreach (Article article in allArticles)
            {
                if (article.Title.Contains('?'))
                {
                    questionsInTitle.Add(article);
                }

                if (article.Abstract.Contains('?'))
                {
                    questionsInAbstract.Add(article);
                }

                if (article.Title.Contains('?') && article.Abstract.Contains('?'))
                {
                    questionsInAbstractAndTitle.Add(article);
                }
            }

            Console.WriteLine("Number of articles with questions in the Title: " + questionsInTitle.Count); // 37
            Console.WriteLine("Number of articles with questions in the Abstract: " + questionsInAbstract.Count); // 40
            Console.WriteLine("Number of articles with questions in the Abstract and the Title: " + questionsInAbstractAndTitle.Count); // 2
            Console.WriteLine();

            foreach (Article article in questionsInTitle)
            {
                Console.WriteLine(article.Title);
                Console.WriteLine();
            }

            /*
            Does your gene need a background check? How genetic background impacts the analysis of mutations, genes, and evolution

            Does Non-Genetic Heterogeneity Facilitate the Development of Genetic Drug Resistance?

            What can topology tell us about the neural code?

            Is neuroscience facing up to statistical power?

            Is Arnold diffusion relevant to global diffusion?

            Can Humans Really Discriminate 1 Trillion Odors?

            Could humans recognize odor by phonon assisted tunneling?

            Where are We in Event-centric Emotion Analysis? Bridging Emotion Role Labeling and Appraisal-based Approaches

            Are task representations gated in macaque prefrontal cortex?

            What Happens to Intentional Concepts in Requirements Engineering If Intentional States Cannot Be Known?

            In whose mind is Mathematics an "a priori cognition"?

            Does Using ChatGPT Result in Human Cognitive Augmentation?

            Can Social Robots Effectively Elicit Curiosity in STEM Topics from K-1 Students During Oral Assessments?

            How do media talk about the Covid-19 pandemic? Metaphorical thematic clustering in Italian online newspapers

            Happy New Year Homo erectus? More evidence for interbreeding with archaics predating the modern human/Neanderthal split

            Is abundant A-to-I RNA editing primate-specific?

            How will the Internet of Things enable Augmented Personalized Health?

            What happens when the geomagnetic field reverses?

            Walking through a library remotely - Why we need maps for collections and how KnoweScape can help us to make them?

            Decision-aid or Controller? Steering Human Decision Makers with Algorithms

            Can SAM recognize crops? Quantifying the zero-shot performance of a semantic segmentation foundation model on generating crop-type maps using satellite imagery for precision agriculture

            Does economics need a scientific revolution?

            Is there currently a scientific revolution in scientometrics?

            A New Scientific Revolution at the Horizon?

            What shapes climate change perceptions in Africa? A random forest approach

            A paradigm shift, or a paradigm adjustment? The evolution of the Oleaceae mating system as a small-scale Kuhnian case-study

            The Nightmare Scenario and the Origin of the Standard Model. "We Got it Wrong ...How did we misread the signals? ... What to Do?"

            Which are the True Defeasible Logics?

            Art or Artifice? Large Language Models and the False Promise of Creativity

            A Robot Walks into a Bar: Can Language Models Serve as Creativity Support Tools for Comedy? An Evaluation of LLMs' Humour Alignment with Comedians

            How do public research labs use funding for research? A case study

            Where is the mind within the brain? Transient selection of subnetworks by metabotropic receptors and G protein-gated ion channels

            How to use magnetic field information for coronal loop identification?

            Bloch Electrons in a Magnetic Field - Why Does Chaos Send Electrons the Hard Way?

            Can Bohmian trajectories account for quantum recurrences having classical periodicities?

            Why does group theory fail to describe charge structure of particles ?

            Magnetic winding: what is it and what is it good for?
            */

            Console.WriteLine();

            //foreach (Article article in questionsInAbstract)
            //{
            //    Console.WriteLine(article.Abstract);
            //    Console.WriteLine();
            //}

            #endregion

            #region paper

            //// In this paper
            //List<Article> inThisPaperInAbstract = new List<Article>(); // 323
            //List<Article> inThisPaperInTitle = new List<Article>(); // 0
            //List<Article> inThisPaperInAbstractAndTitle = new List<Article>();

            //foreach (Article article in allArticles)
            //{
            //    if (article.Abstract.Contains("n this paper"))
            //    {
            //        inThisPaperInAbstract.Add(article);
            //    }
            //    if (article.Title.Contains("n this paper"))
            //    {
            //        inThisPaperInTitle.Add(article);
            //    }
            //    if (article.Title.Contains("n this paper") || article.Abstract.Contains("n this paper"))
            //    {
            //        inThisPaperInAbstractAndTitle.Add(article);
            //    }
            //}


            //// manuscript
            //List<Article> manuscriptInAbstract = new List<Article>(); // 6
            //List<Article> manuscriptInTitle = new List<Article>(); // 6
            //List<Article> manuscriptInAbstractAndTitle = new List<Article>(); // 6

            //foreach (Article article in allArticles)
            //{
            //    if (article.Abstract.Contains("manuscript"))
            //    {
            //        manuscriptInAbstract.Add(article);
            //    }
            //    if (article.Title.Contains("manuscript"))
            //    {
            //        manuscriptInTitle.Add(article);
            //    }
            //    if (article.Title.Contains("manuscript") || article.Abstract.Contains("manuscript"))
            //    {
            //        manuscriptInAbstractAndTitle.Add(article);
            //    }
            //}

            //Console.WriteLine("manuscript !!!!!!!");

            //foreach (Article article in manuscriptInTitle)
            //{
            //    Console.WriteLine(article.Title);
            //    Console.WriteLine();
            //}

            //foreach (Article article in manuscriptInAbstract)
            //{
            //    Console.WriteLine(article.Abstract);
            //    Console.WriteLine();
            //}


            //// answer
            //List<Article> answerInAbstract = new List<Article>(); // 28
            //List<Article> answerPaperInTitle = new List<Article>(); // 0
            //List<Article> answerInAbstractAndTitle = new List<Article>();

            //foreach (Article article in allArticles)
            //{
            //    if (article.Abstract.Contains("answer"))
            //    {
            //        answerInAbstract.Add(article);
            //    }
            //    if (article.Title.Contains("answer"))
            //    {
            //        answerPaperInTitle.Add(article);
            //    }
            //    if (article.Title.Contains("answer") || article.Abstract.Contains("answer"))
            //    {
            //        answerInAbstractAndTitle.Add(article);
            //    }
            //}

            //Console.WriteLine("answer !!!!!!!");
            //foreach (Article article in answerInAbstract)
            //{
            //    Console.WriteLine(article.Abstract);
            //    Console.WriteLine();
            //}


            //// attention
            //List<Article> attentionInAbstract = new List<Article>(); // 85
            //List<Article> attentionInTitle = new List<Article>(); // 0
            //List<Article> attentionInAbstractAndTitle = new List<Article>();

            //foreach (Article article in allArticles)
            //{
            //    if (article.Abstract.Contains("attention"))
            //    {
            //        attentionInAbstract.Add(article);
            //    }
            //    if (article.Title.Contains("attention"))
            //    {
            //        attentionInTitle.Add(article);
            //    }
            //    if (article.Title.Contains("attention") || article.Abstract.Contains("attention"))
            //    {
            //        attentionInAbstractAndTitle.Add(article);
            //    }
            //}

            #endregion

            Console.Read();
        }

    }
}
