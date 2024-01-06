//namespace PizzaStore;

//public record Pizza
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }
//    public string? Description { get; set; }
//}

//public class PizzaDB
//{
//    private static List<Pizza> s_pizzas = new List<Pizza>()
//    {
//        new Pizza { Id=1, Name="Montemagno, Pizza shaped like a great mountain" },
//        new Pizza { Id=2, Name="The Galloway, Pizza shaped like a submarine, silent but deadly" },
//        new Pizza { Id=3, Name="The Noring, Pizza shaped like a Viking helmet, where's the mead" }
//    };

//    public static List<Pizza> GetPizzas() { return s_pizzas; }
//    public static Pizza? GetPizza(int id) { return s_pizzas.SingleOrDefault(pizza => pizza.Id == id); }
//    public static Pizza CreatePizza(Pizza pizza) { s_pizzas.Add(pizza); return pizza; }
//    public static Pizza UpdatePizza(Pizza update)
//    {
//        s_pizzas = s_pizzas.Select(pizza =>
//        {
//            if (pizza.Id == update.Id) { pizza.Name = update.Name; }
//            return pizza;
//        }).ToList();
//        return update;
//    }
//    public static void RemovePizza(int id) { s_pizzas = s_pizzas.FindAll(pizza => pizza.Id != id).ToList(); }
//}
