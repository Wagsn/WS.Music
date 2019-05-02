using System;
using System.Collections.Generic;
using System.Linq;
using WS.Music.Entities;

namespace WS.Music.Stores
{
    /// <summary>
    /// 数据库初始化器
    /// </summary>
    public class DbIntializer
    {
        /// <summary>
        /// 库初始化
        /// </summary>
        /// <param name="context"></param>
        public static void Initialize(MusicDbContext context)
        {
            context.Database.EnsureCreated();

            // 初始化数据
            if (context.Artists.Any() || context.Albums.Any() || context.RelArtistAlbums.Any())
            {
                return;
            }

            #region 艺人
            var xsArtist = Guid.NewGuid().ToString();
            var wslArtist = Guid.NewGuid().ToString();
            var ljjArtist = Guid.NewGuid().ToString();
            var xzqArtist = Guid.NewGuid().ToString();
            var lrhArtist = Guid.NewGuid().ToString();
            var gemArtist = Guid.NewGuid().ToString();
            var xlArtist = Guid.NewGuid().ToString();
            var wlhArtist = Guid.NewGuid().ToString();
            var yzwArtist = Guid.NewGuid().ToString();
            var syzArtist = Guid.NewGuid().ToString();
            var zjArtist = Guid.NewGuid().ToString();
            var zlyArtist = Guid.NewGuid().ToString();
            var psArtist = Guid.NewGuid().ToString();
            var zxzArtist = Guid.NewGuid().ToString();
            var rrArtist = Guid.NewGuid().ToString();
            var zcxArtist = Guid.NewGuid().ToString();
            var hybArtist = Guid.NewGuid().ToString();
            var lygArtist = Guid.NewGuid().ToString();
            var cjyArtist = Guid.NewGuid().ToString();
            var jwqArtist = Guid.NewGuid().ToString();
            var jzwArtist = Guid.NewGuid().ToString();


            var artists = new List<Artist>
                {
                    new Artist
                    {
                        Id = xsArtist,
                        Name = "许嵩",
                        Description = "庐山烟雨浙江潮，未到千般恨不消；及至到来无一事，庐山烟雨浙江潮。",
                        BirthTime = DateTime.Parse("2011-4-1")
                    },
                    new Artist
                    {
                        Id = wslArtist,
                        Name = "汪苏泷",
                        Description = "汪苏泷，内地创作新人，就读于沈阳音乐学院歌曲创作与MIDI制作系，音乐风格上偏多元化，有R&B、中国风、RAP、金属摇滚，等等，将种种时下最流行的音乐元素都融入其创作中。2013年1月25日获得音乐先锋榜“最佳创作新人”大奖。代表作：《桃花扇》、《万有引力》等。",
                        BirthTime = DateTime.Parse("2009-1-1")
                    },
                    new Artist
                    {
                        Id = ljjArtist,
                        Name = "林俊杰",
                        Description = "著名男歌手，作曲人、作词人、音乐制作人，偶像与实力兼具。林俊杰出生于新加坡的一个音乐世家。在父母的引导下，4岁就开始学习古典钢琴，不善言辞的他由此发现了另一种与人沟通的语言。小时候的林俊杰把哥哥林俊峰当作偶像，跟随哥哥的步伐做任何事，直到接触流行音乐后，便爱上创作这一条路。2003年以专辑《乐行者》出道，2004年一曲《江南》红遍两岸三地，凭借其健康的形象，迷人的声线，出众的唱功，卓越的才华，迅速成为华语流行乐坛的领军人物之一，迄今为止共创作数百首音乐作品，唱片销量在全亚洲逾1000万张。2007年成立个人音乐制作公司JFJ，2008创立潮流品牌SMG。2011年被媒体封为“新四大天王”之一，同年8月8日正式加盟华纳音乐，开启事业新乐章。2012年发行故事影像书《记得》，成功跻身畅销书作家行列。获得多项奖项：第15届台湾金曲奖最佳新人奖，6届新加坡金曲奖大奖，6届新加坡词曲版权协会大奖，8届全球华语歌曲排行榜大奖，5届MusicRadio中国TOP排行榜大奖。",
                        BirthTime = DateTime.Parse("2003-1-1")
                    },
                    new Artist
                    {
                        Id =xzqArtist,
                        Name  = "薛之谦",
                        Description = "薛之谦（Joker Xue），1983年7月17日出生于上海，华语流行乐男歌手、影视演员、音乐制作人，毕业于格里昂酒店管理学院。 2005年，参加选秀节目《我型我秀》正式出道 。2006年，发行首张同名专辑《薛之谦》，随后凭借歌曲《认真的雪》获得广泛关注。2008年，发行专辑《深深爱过你》并在上海举行个人首场演唱会“谦年传说” 。2013年，专辑《几个薛之谦》获得MusicRadio中国TOP排行榜内地推荐唱片。2014年，专辑《意外》获得第21届东方风云榜颁奖最佳唱作人 ；2015年6月，薛之谦首度担当制作人并发行原创EP《绅士》，2016年，凭借EP《绅士》、《一半》获得第16届全球华语歌曲排行榜多项奖项，而歌曲《初学者》则获得年度二十大金曲奖。2017年4月，开启“我好像在哪见过你”全国巡回演唱会。",
                        BirthTime = DateTime.Parse("2005-1-1")
                    },
                    new Artist
                    {
                        Id = lrhArtist,
                        Name = "李荣浩",
                        Description = "李荣浩，1985年7月11日生于蚌埠，中国流行音乐制作人、歌手、吉他手。曾为众多艺人创作歌曲以及担任制作人，也曾为多部电影与多款电子游戏制作音乐。2013年9月17日发行个人首张原创专辑《模特》，凭借这张专辑入围第25届金曲奖最佳国语男歌手奖、最佳新人奖、最佳专辑制作人、最佳国语专辑、最佳作词奖等五项大奖提名，成为最大黑马，实现了从制作人到歌手的华丽转身。",
                        BirthTime =DateTime.Parse("2013-9-17")
                    },
                    new Artist
                    {
                        Id = gemArtist,
                        Name ="G.E.M.邓紫棋",
                        Description = "邓紫棋成长于一个音乐世家，其母亲为上海音乐学院声乐系毕业生，外婆教唱歌，舅父拉小提琴，外公在乐团吹色士风。在家人的熏陶下，自小便热爱音乐，喜爱唱歌，与音乐结下不解之缘。G.E.M.在5岁的时候已经开始尝试作曲及填词，13岁完成了8级钢琴。G.E.M.在小学时期就读位于太子道西的中华基督教会协和小学上午校，为2003年的毕业生，同时亦为校内诗歌班成员。其英文名G.E.M.是Get Everybody Moving的缩写，象徵著她希望透过音乐让大家动起来的梦想。2008年出道，2009年在叱咤乐坛流行榜颁奖典礼夺得女新人奖金奖，亦是该奖项历年来最年轻，以及第一位未成年的得奖者。因其广阔的音域，得到不少前辈歌手点名公开赞扬。2014年她参加湖南卫视《我是歌手》第二季名声大震。",
                        BirthTime = DateTime.Parse("2008-1-1")
                    },
                    new Artist
                    {
                        Id = xlArtist,
                        Name ="徐良",
                        Description = "内地青年唱作歌手。没有唱片公司背景、无制作团队，自己包揽词曲创作制作，徐良自编自唱的《犯贱》《红装》《和平分手》等歌曲在互联网上悄然通过口碑的方式走红，深受好评，打开知名度。2011年签约滚石移动美妙音乐公司并在2012年发行首张专辑《不良少年》。继首张个人大碟《不良少年》于2012年3月发行之后，徐良再接再厉，于6月又推新单曲《无颜女》。代表作品：《犯贱》《客官不可以》《无颜女》《考试什么的都去死吧》。",
                        BirthTime = DateTime.Parse("2011-1-1")
                    },
                    new Artist
                    {
                        Id = wlhArtist,
                        Name = "王力宏",
                        Description = "中国著名流行歌手、词曲创作音乐人；亚洲华语流行乐坛最具代表性的创作、偶像、实力派音乐偶像巨星。1995年发行首张专辑《情敌贝多芬》在台湾出道，亦是金曲奖中最年轻的封王者，其唱片总销量在全亚洲已超过2500万张。从改编歌曲《龙的传人》融合西方电子节奏和东方旋律的华人中式嘻哈风，再到为华语流行乐注入新元素，进一步将其推向全世界。超高唱片销量便可以证明力宏的影响力毋庸置疑的强。况且身为金曲奖常客，3次接受CNN电视台访问。首创Chinked-out曲风，将中国风推向世界，又用自己写的歌揭露了娱乐圈的众多丑闻，都可以表现他对华语乐坛的巨大贡献。亦参与多部电影工作，2010年进军影坛，首部自导自演的电影《恋爱通告》票房纪录突破5000万，成为票房冠军，其导演才华受到了广大好评。",
                        BirthTime = DateTime.Parse("1995-1-1")
                    },
                    new Artist
                    {
                        Id = yzwArtist,
                        Name = "杨宗纬",
                        Description = "大学时期参加台湾歌唱选秀节目《超级星光大道》获选为第一届“人气王”。比赛的时候，杨宗纬歌声阳刚而细腻，富含感情，辨识度高，感染力强，以演唱抒情歌曲见长，选曲跨越性别界线，无论是男女歌手的抒情歌曲，经过他重新诠释之后，常常都可以得到不输原唱或甚至超越原唱的评价。出道后屡创多项记录，包括发行首张专辑，便以新人之姿登上台北小巨蛋举办个人演唱会，也是唯一一位创下2年内包办“三金一亚”（台湾金曲奖，金钟奖，金马奖及亚太影展）颁奖典礼演唱嘉宾的歌手。歌声阳刚而细腻，有高度感染力与辨识度，富含感情，被誉为“催泪歌神”，又昵称“鸽王”。",
                        BirthTime =DateTime.Parse("2007-01-05")
                    },
                    new Artist
                    {
                        Id = syzArtist,
                        Name = "孙燕姿"
                    },
                    new Artist
                    {
                        Id = zjArtist,
                        Name = "张杰"
                    },
                    new Artist
                    {
                        Id = zlyArtist,
                        Name ="张靓颖"
                    },
                    new Artist
                    {
                        Id = psArtist,
                        Name = "朴树"
                    },
                    new Artist
                    {
                        Id =zxzArtist,
                        Name = "张信哲"
                    },
                    new Artist
                    {
                        Id = rrArtist,
                        Name = "任然"
                    },
                    new Artist
                    {
                        Id = zcxArtist,
                        Name = "周传雄"
                    },
                    new Artist
                    {
                        Id = hybArtist,
                        Name = "胡彦斌"
                    },
                    new Artist
                    {
                        Id =lygArtist,
                        Name = "李玉刚"
                    },
                    new Artist
                    {
                        Id = cjyArtist,
                        Name = "蔡健雅"
                    },
                    new Artist
                    {
                        Id = jwqArtist,
                        Name = "金玟岐"
                    },
                    new Artist
                    {
                        Id =jzwArtist,
                        Name = "金志文"
                    }
                };
            context.AddRange(artists);

            #endregion

            #region 专辑
            var fhjdAlbum = Guid.NewGuid().ToString();
            var fcynAlbum = Guid.NewGuid().ToString();
            var xbyxAlbum = Guid.NewGuid().ToString();
            var wlyAlbum = Guid.NewGuid().ToString();
            var hddsjAlbum = Guid.NewGuid().ToString();
            var tgAlbum = Guid.NewGuid().ToString();
            var sysdAlbum = Guid.NewGuid().ToString();
            var jnyAlbum = Guid.NewGuid().ToString();
            var jhAlbum = Guid.NewGuid().ToString();
            var qnwbAlbum = Guid.NewGuid().ToString();
            var byAlbum = Guid.NewGuid().ToString();
            var sxnhAlbum = Guid.NewGuid().ToString();
            var qgAlbum = Guid.NewGuid().ToString();
            var brccqAlbum = Guid.NewGuid().ToString();
            var sszjAlbum = Guid.NewGuid().ToString();
            var wzdwAlbum = Guid.NewGuid().ToString();
            var xfnmsmdblAlbum = Guid.NewGuid().ToString();
            var myjAlbum = Guid.NewGuid().ToString();
            var tlbbAlbum = Guid.NewGuid().ToString();
            var sglmydAlbum = Guid.NewGuid().ToString();
            var syAlbum = Guid.NewGuid().ToString();
            var bcysAlbum = Guid.NewGuid().ToString();
            var xwqsAlbum = Guid.NewGuid().ToString();
            var zdyAlbum = Guid.NewGuid().ToString();
            var wxqndsAlbumm = Guid.NewGuid().ToString();
            var qlzAlbum = Guid.NewGuid().ToString();
            var xsdqjAlbum = Guid.NewGuid().ToString();
            var dqcxAlbum = Guid.NewGuid().ToString();
            var snddbAlbum = Guid.NewGuid().ToString();
            var xhyAlbum = Guid.NewGuid().ToString();
            var vAlbum = Guid.NewGuid().ToString();

            var albums = new List<Album>
            {
                new Album
                {
                    Id = fhjdAlbum,
                    Name = "绝代风华",
                    Description = "《绝代风华》单曲介绍 2018继发行第七张个人全创作专辑《寻宝游戏》后，许嵩受邀为游戏【天下3】创作出全新主题曲《绝代风华》，暌违数年的“许氏中国风”再现江湖。 《绝代风华》的基调是逍遥自在的：主人公曾“鲜衣怒马”、“征战杀伐”，有了所爱之人后便告别江湖、悠然归隐，与知己落棋听雨、朝夕为伴、互赏风华。特别之处是开篇以悬疑气氛入题，描摹不速之客在林间暗伏之景——许嵩用寥寥数语就为笔下人物创造出一种“隐逸中带有少许凶险”的生活状态，让人物鲜活起来。生动的叙事性、严谨的逻辑性、造词与用典的准确性，这三者是许嵩国风类词作的一贯特性。 音乐方面，许嵩的国风音乐一贯强调律动与节奏，以电鼓与民族大鼓相交叠的方式，让听感自由穿梭于中西之间，毫不拘谨；古筝琵琶笛子与失真吉他熔于一炉，古今交错，擦除时代感。主副歌的旋律创作采用由快到慢、由短句到长句的对比手法，呼应词里由愉悦喝茶下棋到深情回忆往事的情绪变化，丝丝入扣。词、曲、唱、制作紧密咬合，达到浑然一体的程度。",
                    ReleaseTime = DateTime.Parse("2018-11-22")
                },
                new Album
                {
                        Id = fcynAlbum,
                        Name = "飞驰于你",
                        Description = "敦煌作为古代丝绸之路的重要节点，东西方文化在这里汇聚碰撞，更拥有着莫高窟、阳关、月牙泉等古迹名胜，是世界级的文化名城。近日QQ飞车手游推出敦煌主题版本，邀请许嵩创作并演唱全新主题曲《飞驰于你》。\r\n\r\n《飞驰于你》由许嵩包揽词曲创作及制作。因敦煌在古时即传入了西域文化，许嵩在作曲中特意融入西域音乐元素，以富有异域风情的曲调展现敦煌的神秘；偶尔点缀的转音又带有甘肃敦煌当地的戏曲特色，契合了敦煌作为多民族文化荟萃之地的特质；民族音乐鼓点与摇滚乐新鲜搭配，以强劲的节奏感展现飞驰于敦煌奇景时的快意。\r\n\r\n词作方面，对意象进行精心拣选，寥寥数语即构建出敦煌秘境。在展现飞驰时的速度与激情之外，以“你”指代敦煌，抒发对敦煌厚重历史的感慨，令整首作品轻松潇洒而又不失余味。\r\n\r\n近年来许嵩为大量热门游戏创作主题音乐，每一次的创作都实现了既贴合主题气质、又淋漓尽致展现个人风格，并且始终坚持创新、诚意十足，不断给乐迷带来全新的听觉体验。",
                        ReleaseTime = DateTime.Parse("2018-09-07")
                    },
                    new Album
                    {
                        Id = xbyxAlbum,
                        Name = "寻宝游戏",
                        Description = "2018年许嵩最新作品《寻宝游戏》，许嵩第7张词曲全创作专辑。\r\n\r\n你我他她一生在世，寻寻觅觅各有所求，人人心中皆有秘密宝藏。许嵩在《寻宝游戏》中以松弛、真实的笔触书写对生活的体悟以及对世态的思考，寻访人心、玩味世情。\r\n\r\n《寻宝游戏》的语言系统中充满着多义与辨证，让读者在聆听的过程中不知不觉沉浸于许嵩构建的这一场头脑游戏，富有诗性与灵气的文字挑战歌词创作新高度。他的文字里时而带着严谨理性的刚硬质地，但细听之下又可体会他的幽默与柔软——这份既刚又柔、亦庄亦谐的矛盾感形成了某种难以言喻的、专属于许嵩的艺术效果。\r\n\r\n在《寻宝游戏》里许嵩以极致复古的音乐语言带领听者回到20世纪60-80年代，融合Blues-Rock、Country-Rock、Folk-Rock、Electronic-Rock、Jazz等根源性音乐风格，加以最新锐的制作理念打造当代音乐精品，既复古，又超前。他旧时明月般的老灵魂，总是能找到极其鲜活的表达方式来完成情感与观点的输出。整张专辑音乐类型多元化却又统一于许嵩强大的音乐制作和统筹功力，在听觉层面达到了惊人的一体性。许嵩以松弛自然的演唱方式唱出独属于他的腔调与态度，无可拷贝。音乐里精心设置的诸多细节伏笔也有待听者去开掘玩味，这是《寻宝游戏》所引发的寻宝游戏。\r\n\r\n《寻宝游戏》的平面相片及音乐录影带拍摄于美国纽约及美国西海岸地区。二十余人团队同许嵩一起出发，海外拍摄花费长达半个月的时间。专辑封面相片拍摄于美国西海岸地区1号公路，牛仔青年，开启寻宝之旅。以橙色为主色的美式唱片装帧，在复古中加入摩登元素，设计考究。专辑曲目《老古董》、《大千世界》、《如约而至》的音乐录影带摄于纽约，由台湾著名MV导演比尔贾执导，影像画面寓意丰富，余味悠长。\r\n\r\n《寻宝游戏》专辑概念由许嵩一手打造，并且每首曲目的词曲创作、音乐制作均由许嵩独自完成。至今为止许嵩已一人作词、作曲、演唱、制作、策划7张个人专辑，这份成绩与其全方位的音乐才华在华语乐坛是独树一帜的。2018年是许嵩进入公众视野的第十二年，成功发行7张词曲全创作专辑以及近40首原创单曲，勤勉专注于音乐创作；屡获国内外各大华语音乐颁奖典礼奖项，拥有突破十亿次的数字音乐下载量，影响力强大；2017年开启万人巡演，场场爆满座无虚席，从头至尾全程万人大合唱；在斐然的成绩面前，许嵩始终远离喧嚣保持低调姿态，只靠作品说话，在《寻宝游戏》里进一步展现了广阔的音乐视野与深厚的人文积淀。",
                        ReleaseTime = DateTime.Parse("2018-07-12")
                    },
                    new Album
                    {
                        Id = wlyAlbum ,
                        Name = "我乐意",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2018-04-19")
                    },
                    new Album
                    {
                        Id = hddsjAlbum,
                        Name = "蝴蝶的时间",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2017-11-30")
                    },
                    new Album
                    {
                        Id = tgAlbum,
                        Name = "通关",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2017-06-30")
                    },
                    new Album
                    {
                        Id = sysdAlbum,
                        Name = "深夜书店",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2017-03-14")
                    },
                    new Album
                    {
                        Id = jnyAlbum,
                        Name = "今年勇",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2016-12-22")
                    },
                    new Album
                    {
                        Id = jhAlbum,
                        Name = "江湖",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2016-08-31")
                    },
                    new Album
                    {
                        Id = qnwbAlbum,
                        Name = "青年晚报",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2016-06-27")
                    },
                    new Album
                    {
                        Id = byAlbum,
                        Name = "不语",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2016-03-03")
                    },
                    new Album
                    {
                        Id = sxnhAlbum,
                        Name = "书香年华",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2016-02-25")
                    },
                    new Album
                    {
                        Id = qgAlbum,
                        Name = "千古",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2015-08-20")
                    },
                    new Album
                    {
                        Id = brccqAlbum,
                        Name = "不如吃茶去",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2014-08-23")
                    },
                    new Album
                    {
                        Id = sszjAlbum,
                        Name = "山水之间",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2014-07-15")
                    },
                    new Album
                    {
                        Id = wzdwAlbum,
                        Name = "违章动物",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2013-08-22")
                    },
                    new Album
                    {
                        Id = xfnmsmdblAlbum,
                        Name = "小烦恼没什么大不了",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2013-05-21")
                    },
                    new Album
                    {
                        Id = myjAlbum,
                        Name = "梦游记",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2012-07-11")
                    },
                    new Album
                    {
                        Id = tlbbAlbum,
                        Name = "天龙八部之宿敌",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2011-09-2")
                    },
                    new Album
                    {
                        Id = sglmydAlbum,
                        Name = "苏格拉没有底",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2011-04-01")
                    },
                    new Album
                    {
                        Id = syAlbum,
                        Name = "素颜",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2010-08-18")
                    },
                    new Album
                    {
                        Id = bcysAlbum,
                        Name = "半城烟沙",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2010-04-11")
                    },
                    new Album
                    {
                        Id = xwqsAlbum,
                        Name = "寻雾启示",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2010-01-06")
                    },
                    new Album
                    {
                        Id = zdyAlbum,
                        Name = "自定义",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2009-01-10")
                    },
                    new Album
                    {
                        Id = wxqndsAlbumm,
                        Name = "我想牵你的手",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2008-04-22")
                    },
                    new Album
                    {
                        Id = qlzAlbum,
                        Name = "情侣装",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2008-01-15")
                    },
                    new Album
                    {
                        Id = dqcxAlbum,
                        Name = "断桥残雪",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2007-12-28")
                    },
                    new Album
                    {
                        Id = snddbAlbum,
                        Name = "送你的独白",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2007-12-04")
                    },
                    new Album
                    {
                        Id = xhyAlbum,
                        Name = "雪花谣",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2007-11-28")
                    },
                    new Album
                    {
                        Id = vAlbum,
                        Name = "V",
                        Description = "",
                        ReleaseTime = DateTime.Parse("2007-05-24")
                    }
                };
            context.AddRange(albums);
            #endregion

            // 艺人专辑
            var relArtistAlbums = new List<RelArtistAlbum>
            {
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =fhjdAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =fcynAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =xbyxAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =wlyAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =hddsjAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =tgAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =sysdAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =jnyAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =jhAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =qnwbAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =byAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =sxnhAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =qgAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =brccqAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =sszjAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =wzdwAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =xfnmsmdblAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =myjAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =tlbbAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =sglmydAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =syAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =bcysAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =xwqsAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =zdyAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =wxqndsAlbumm
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =qlzAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =xsdqjAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =dqcxAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =snddbAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =xhyAlbum
                },
                new RelArtistAlbum
                {
                    Id = Guid.NewGuid().ToString(),
                    ArtistId = xsArtist,
                    AlbumId =vAlbum
                }
            };
            context.AddRange(relArtistAlbums);
            // 歌曲专辑
            var relSongAlbums = new List<RelSongAlbum>
            {

            };

            context.SaveChanges();
        }
    }
}
