namespace ClubMembership.Migrations
{
    using ClubMembership.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ClubMembership.DAL.MembershipContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ClubMembership.DAL.MembershipContext context)
        {
            //var Members = new List<Member>
            //{
            //    new
            //    Member{FirstName="Vlado",LastName="Kopiæ",MembershipDate=DateTime.Parse("2013-12-05"),DateOfBirth= DateTime.Parse("1989-12-05")},
   
                
            //    };
            //Members.ForEach(s => context.Members.AddOrUpdate(p => p.LastName, s));
            //context.SaveChanges();
            //var Editions = new List<Edition>
            //    {
            //    new Edition{EditionId=1,Title="1st Edition",Description="The original D&D was published as a box set in 1974 and featured only a handful of the elements for which the game is known today: just three character classes (fighting-man, magic-user, and cleric); four races (human, dwarf, elf, and hobbit); only a few monsters; only three alignments (lawful, neutral, and chaotic)."},
            //    new Edition{EditionId=2,Title="2nd Edition",Description="An effort was made to remove aspects of the game which had attracted negative publicity, most notably the removal of all mention of demons and devils, although equivalent fiendish monsters were included, renamed tanar'ri and baatezu, respectively. Moving away from the moral ambiguity of the 1st edition AD&D, the TSR staff eliminated character classes and races like the assassin and the half-orc, and stressed heroic roleplaying and player teamwork. The target age of the game was also lowered, with most 2nd edition products being aimed primarily at teenagers."},
            //    new Edition{EditionId=3,Title="3rd Edition",Description="A major revision of the AD&D rules was released in 2000. As the Basic game had been discontinued some years earlier, and the more straightforward title was more marketable, the word advanced was dropped and the new edition was named just Dungeons & Dragons, but still officially referred to as 3rd edition (or 3E for short). It is the basis of a broader role-playing system designed around 20-sided dice, called the d20 System."},
            //    new Edition{EditionId=4,Title="3.5th Edition",Description="In July 2003, a revised version of the 3rd edition D&D rules (termed v. 3.5) was released that incorporated numerous small rule changes, as well as expanding the Dungeon Master's Guide and Monster Manual. This revision was intentionally a small one focusing on addressing common complaints about certain aspects of gameplay, hence the half edition version number. The basic rules are fundamentally the same, and many monsters and items are compatible (or even unchanged) between those editions. New spells are added, and numerous changes are made to existing spells, while some spells are removed from the updated Player's Handbook. New feats are added and numerous changes are made to existing feats, while several skills are renamed or merged with other skills."},
            //    new Edition{EditionId=5,Title="4th Edition",Description="Mechanically, 4th edition saw a major overhaul of the game’s systems. Changes in spells and other per-encounter resourcing, giving all classes a similar number of at-will, per-encounter and per-day powers. Powers have a wide range of effects including inflicting status effects, creating zones, and forced movement, making combat very tactical for all classes but essentially requiring use of miniatures, reinforced by the use of squares to express distances. Attack rolls, skill checks and defense values all get a bonus equal to one-half level, rounded down, rather than increasing at different rates depending on class or skill point investment. Each skill is either trained (providing a fixed bonus on skill checks, and sometimes allowing more exotic uses for the skills) or untrained, but in either case all characters also receive a bonus to all skill rolls based on level. A system of “healing surges” and short and long rests are introduced to act as resource management."},
            //    new Edition{EditionId=6,Title="5th Edition",Description="Mechanically, 5th edition draws heavily on prior editions, while introducing some new mechanics intended to simplify ease of play. Actions are now more dependent on checks made with the six core abilities with skills taking a more supportive role. Skills, weapons, items, saving throws and other things that a character is trained in (proficient) now all use a single proficiency bonus that increases as level increases. Multiple defense values have been removed, returning to a single defense value of armor class and using more traditional saving throws."}
            //    };
            //Editions.ForEach(s => context.Editions.AddOrUpdate(p => p.Title, s));
            //context.SaveChanges();
            //var Campaigns = new List<Campaign>
            //    {
            //    new Campaign{Title="Curse Of Stradh",EditionId=6,Level=12, Members = new List<Member>()},
            //    new Campaign{Title="Out of the Abyss",EditionId=6,Level=17, Members = new List<Member>()},
            //    new Campaign{Title="Lost Mine of Phandelver",EditionId=6,Level=2, Members = new List<Member>()},
            //    new Campaign{Title="Lord of the Iron Fortress",EditionId=3,Level=17, Members = new List<Member>()}
            //    };
            //Campaigns.ForEach(s => context.Campaigns.AddOrUpdate(p => p.Title, s));
            //context.SaveChanges();
        }
    }
}
