

namespace VismaProject{

    public class AllShortage{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public string Category { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedOn { get; set; }

        public AllShortage(int id, string title, string name,string room,string category,int priority,DateTime created){
            Id=id;
            Title=title;
            Name=name;
            Room=room;
            Category=category;
            Priority=priority;
            CreatedOn=created;
        }

    }
}
