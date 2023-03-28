
using System.IO;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;



namespace VismaProject{
    class User{    
        public string username;
        bool isUserListEmpty;
        private List<AllShortage> allShortage = new List<AllShortage>();
        private int id=1;
        public User()
        {   jsonToC();                //constructor
            getUsername();
        }

        private void jsonToC()
{
            // Create a JSON file to store shortage information
            string filePath = @"C:\Users\Asus\Dropbox\My_PC\Documents\PaSkAiTu_StUfF\praktikos_stuff_c#\VismaProject\shortages.json";
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                if(json.Length<10)
                    return;
                allShortage = JsonConvert.DeserializeObject<List<AllShortage>>(json);
                id=allShortage.Last().Id;
                
                return;
            }
            File.WriteAllText(filePath, "[]");
            
}
        private void end(){
            string filePath = @"C:\Users\Asus\Dropbox\My_PC\Documents\PaSkAiTu_StUfF\praktikos_stuff_c#\VismaProject\shortages.json";
            allShortage=allShortage.OrderBy(a => a.Id).ToList();           //sorting by id
            string json = JsonConvert.SerializeObject(allShortage);
            File.WriteAllText(filePath, json);
            Console.WriteLine("the database have been updated. Have a nice day ^^");
            pause();
    }

        private int checkIfInt(){
            int res;
            while (!int.TryParse(Console.ReadLine(), out res)){             //this class makes sure that user inputs int
                Console.WriteLine("Invalid input. Please enter a valid number:");
            }
            return res;
        }

        private bool showList(){
            isUserListEmpty=false;
            if(username=="admin"){
                    foreach(var item in allShortage){
                        Console.WriteLine($"id of request: {item.Id} title: {item.Title} name: {item.Name} room: {item.Room} category: {item.Category} priority: {item.Priority} created date: {item.CreatedOn}");
                        isUserListEmpty=true;
                    }
                }


                else{
                    foreach(var item in allShortage){
                        if(item.Name==username){
                            Console.WriteLine($"id of request: {item.Id} title: {item.Title} name: {item.Name} room: {item.Room} category: {item.Category} priority: {item.Priority} created date: {item.CreatedOn}");
                            isUserListEmpty=true;
                        }
                    }
                }
                if(isUserListEmpty)
                    return false;
                else{
                    Console.WriteLine("in database there is nothing for this user");
                    pause();
                    return true;
                }

        }
        private void AddNewShortage(){
            Console.Clear();
            Console.WriteLine("Title of shortage:");
            string title=Console.ReadLine();
            Console.Clear();

            int tempInt;
            string room="";
            string category="";
            int priority;
            DateTime createdOn = DateTime.Now;
            bool flag;


            while(true){
            Console.WriteLine("which room? 1)Meeting Room 2)Kitchen 3)Bathroom  (enter number)");
            tempInt=checkIfInt();
                switch(tempInt){       //changing number to a room name
                    case 1:
                    room="meeting room";
                    break;
                    case 2:
                    room="kitchen";
                    break;
                    case 3:
                    room="bathroom";
                    break;
                    default:
                    Console.Clear();
                    Console.WriteLine("incorrect input. Please enter valid number");
                    break;
                }
                if(room!="")
                    break;

            }
            Console.Clear();
            
            while(true){
            Console.WriteLine("what category? 1)Electronics 2)Food 3)Other  (enter number)");
            tempInt=checkIfInt();
                switch(tempInt){       //changing number to a category 
                    case 1:
                    category="electronics";
                    break;
                    case 2:
                    category="food";
                    break;
                    case 3:
                    category="other";
                    break;
                    default:
                    Console.Clear();
                    Console.WriteLine("incorrect input. Please enter valid number");
                    break;
                }
                if(category!="")
                    break;

            }
            Console.Clear();

            while(true){
                Console.WriteLine("What is the priority? (enter number between 1-10 where 1 is low and 10 is highest)");
                priority=checkIfInt();
                if(priority>=0&&priority<=10){
                    break;
                }
                Console.Clear();
                Console.WriteLine("please enter valid priority");
            }


            flag=false;
            foreach(var item in allShortage){
                if(item.Title==title&&item.Room==room&&item.Priority<priority){
                    Console.WriteLine($"The shortage already exists, but priority were rearranged from {item.Priority} to {priority} and changed category from {item.Category} to {category}");
                    item.Priority=priority;
                    item.Category=category;
                    flag=true;
                    break;
                }
                else if(item.Title==title&&item.Room==room&&item.Priority>=priority){
                    Console.WriteLine($"The shortage already exists");
                    flag=true;
                    break;
                }
            }
            Console.WriteLine($"{title}, {username}, {room}, {category}, {priority}, {createdOn}");
            if(!flag){
                id++;
                var newShortage = new AllShortage(id,title,username,room,category,priority,createdOn);
                allShortage.Add(newShortage); // add new shortage to the list
                Console.WriteLine("New shortage was successfully added!");
            }
            pause();

            }

            private void pause(){
                Console.WriteLine("press any key to continue...");
                Console.ReadKey();
            }

            private void delete(){
                if(isListEmpty("delete"))
                    return;
                Console.WriteLine("The request you are allowed to delete:");
                if(showList()){
                    return;
                }
                Console.WriteLine("please write the id of request you would like to delete:");
                int deleteId = checkIfInt();
                int indexToDelete = allShortage.FindIndex(r => r.Id == deleteId);   //finds index of element to delete
                if (indexToDelete >= 0 && allShortage[indexToDelete].Name==username||username=="admin"){
                    allShortage.RemoveAt(indexToDelete);
                    Console.WriteLine($"Request with ID {deleteId} has been deleted.");
                    pause();
                }
                else{
                    Console.WriteLine($"Request with ID {deleteId} not found.");
                    pause();
                }
            }

            private void filter(){
                int choise;
                if(isListEmpty("show"))
                    return;
                allShortage=allShortage.OrderByDescending(a => a.Priority).ToList();     //sorting list by priority
                while(true){
                    Console.WriteLine("By what parameter you would like to filter list? (select a number)\n1) By title\t2) In date range\t3) By category\t4)By room");
                    choise=checkIfInt();
                    switch(choise){
                        case 1:
                        filterByTitle();
                        break;

                        case 2:
                        filterByDate();
                        break;

                        case 3:
                        filterByCategory();
                        break;

                        case 4:
                        filterByRoom();
                        break;

                        default:
                        break;


                    }


                    if(choise>=1&&choise<=4)
                        break;
                    Console.Clear();
                    Console.WriteLine("incorrect input.");

                }
            }

            private void filterByTitle(){
                Console.WriteLine("Please write the keywords with which to search:");
                string search=Console.ReadLine();
                char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
                string[] keywords = search.Split(delimiterChars);

                if(username=="admin"){
                    foreach(var item in allShortage){
                    for(int i=0;i<keywords.Length;i++){
                        if(item.Title.Contains(keywords[i], System.StringComparison.CurrentCultureIgnoreCase)){
                            Console.WriteLine($"id of request: {item.Id} title: {item.Title} name: {item.Name} room: {item.Room} category: {item.Category} priority: {item.Priority} created date: {item.CreatedOn}");
                            break;
                        }
                    }
                }
                }


                else{
                    foreach(var item in allShortage){
                        if(item.Name==username){
                            for(int i=0;i<keywords.Length;i++){
                                if(item.Title.Contains(keywords[i], System.StringComparison.CurrentCultureIgnoreCase)){
                                    Console.WriteLine($"id of request: {item.Id} title: {item.Title} name: {item.Name} room: {item.Room} category: {item.Category} priority: {item.Priority} created date: {item.CreatedOn}");
                                    break;
                                }
                            }
                        }
                    }
                }
                pause();
                
                

            }

            private void filterByDate(){
                Console.WriteLine("write date(yyyy,mm,dd) from when to search:");
                DateTime fromDate =Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("write date(yyyy,mm,dd) till when to search:");
                DateTime toDate =Convert.ToDateTime(Console.ReadLine());
                
                if(username=="admin"){
                    foreach(var item in allShortage){
                        if(item.CreatedOn<toDate && item.CreatedOn>fromDate){
                            Console.WriteLine($"id of request: {item.Id} title: {item.Title} name: {item.Name} room: {item.Room} category: {item.Category} priority: {item.Priority} created date: {item.CreatedOn}");
                        }
                    }
                }


                else{
                    foreach(var item in allShortage){
                        if(item.Name==username){
                            if(item.CreatedOn<toDate && item.CreatedOn>fromDate){
                                Console.WriteLine($"id of request: {item.Id} title: {item.Title} name: {item.Name} room: {item.Room} category: {item.Category} priority: {item.Priority} created date: {item.CreatedOn}");
                            }
                        }
                    }
                }
                pause();
            }

            private void filterByCategory(){
                int tempInt;
                string category="";
            
            while(true){
            Console.WriteLine("what category? 1)Electronics 2)Food 3)Other  (enter number)");
            tempInt=checkIfInt();
                switch(tempInt){       //changing number to a category 
                    case 1:
                    category="electronics";
                    break;
                    case 2:
                    category="food";
                    break;
                    case 3:
                    category="other";
                    break;
                    default:
                    Console.Clear();
                    Console.WriteLine("incorrect input. Please enter valid number");
                    break;
                }
                if(category!="")
                    break;

            }
            Console.Clear();

            if(username=="admin"){
                    foreach(var item in allShortage){
                        if(item.Category==category){
                            Console.WriteLine($"id of request: {item.Id} title: {item.Title} name: {item.Name} room: {item.Room} category: {item.Category} priority: {item.Priority} created date: {item.CreatedOn}");
                        }
                    }
                }


                else{
                    foreach(var item in allShortage){
                        if(item.Name==username){
                            if(item.Category==category){
                                Console.WriteLine($"id of request: {item.Id} title: {item.Title} name: {item.Name} room: {item.Room} category: {item.Category} priority: {item.Priority} created date: {item.CreatedOn}");
                            }
                        }
                    }
                }
                pause();

            }

            private void filterByRoom(){
                int tempInt;
                string room="";

                while(true){
                Console.WriteLine("which room? 1)Meeting Room 2)Kitchen 3)Bathroom  (enter number)");
                tempInt=checkIfInt();
                    switch(tempInt){       //changing number to a room name
                        case 1:
                        room="meeting room";
                        break;
                        case 2:
                        room="kitchen";
                        break;
                        case 3:
                        room="bathroom";
                        break;
                        default:
                        Console.Clear();
                        Console.WriteLine("incorrect input. Please enter valid number");
                        break;
                    }
                    if(room!="")
                        break;

                }
                Console.Clear();

                if(username=="admin"){
                    foreach(var item in allShortage){
                        if(item.Room==room){
                            Console.WriteLine($"id of request: {item.Id} title: {item.Title} name: {item.Name} room: {item.Room} category: {item.Category} priority: {item.Priority} created date: {item.CreatedOn}");
                        }
                    }
                }


                else{
                    foreach(var item in allShortage){
                        if(item.Name==username){
                            if(item.Room==room){
                                Console.WriteLine($"id of request: {item.Id} title: {item.Title} name: {item.Name} room: {item.Room} category: {item.Category} priority: {item.Priority} created date: {item.CreatedOn}");
                            }
                        }
                    }
                }
                pause();
            }

            private bool isListEmpty(string word){
                if(allShortage.Count==0){
                    Console.WriteLine($"we're lucky and we have everything! There is nothing to {word}");
                    pause();
                    Console.Clear();
                    return true;
                }
                else
                    return false;
            }

            private void getUsername(){
                username=Console.ReadLine();
                if(username=="")
                    username="Guest";
            }

            public void menu(){
                int command;
                while (true){
                Console.WriteLine("Enter a command:");
                Console.WriteLine("1. Register a new shortage");
                Console.WriteLine("2. Delete a shortage");
                Console.WriteLine("3. List all shortages");
                Console.WriteLine("4. Change username");
                Console.WriteLine("5. Exit");

                command = checkIfInt();
                Console.Clear();
                switch(command){
                    case 1:
                    AddNewShortage();
                    break;

                    case 2:
                    delete();
                    break;

                    case 3:
                    filter();
                    break;

                    case 4:
                    Console.WriteLine("New user name:");
                    getUsername();
                    Console.WriteLine($"User name was successfully changed to {username}");
                    pause();
                    break;

                    case 5:
                    end();
                    return;

                    default:
                    Console.WriteLine("we dont have that kind of feature yet. Stay calm and select feature form list ^^");
                    pause();
                    break;


                }
            Console.Clear();
            }
            }



        }
    }
