#include <iostream>
#include <vector>
#include <string>

class Product {
public: 
    int id;
    std::string name;
    double price;
};

class Catalog{
public:
    std::vector<Product> catalog;  

    void load_catalog() {
      catalog.push_back({1, "Apple", 0.5});
      catalog.push_back({2, "Bread", 1.5});
      catalog.push_back({3, "Milk", 2.0});
      catalog.push_back({4, "Chocolate", 1.0});
}

    Product* find_product(int id) {
    // A range-based for loop (C++11+).
    // auto is a type placeholder
    // auto &p declares p as a reference to each element (not a copy)
      for (auto &p : catalog){
            if (p.id == id) return &p;
      }
      return nullptr;
    }
};

class Cart{
public:
    std::vector<int> cart_ids;      
    std::vector<int> cart_qty;      
    
void add_to_cart(int id, int qty) {
  for (size_t i=0;i<cart_ids.size();++i) {
      if (cart_ids[i] == id) { 
        cart_qty[i] += qty; 
        return;
        }
      }
      cart_ids.push_back(id);
      cart_qty.push_back(qty);
  }
};

class PricingCheckout{
public:
    double TAX_RATE = 0.07;       

    double compute_subtotal(const Cart& cart, Catalog& catalog) {
        double s = 0.0;
      for (size_t i=0;i<cart.cart_ids.size();++i) {
        Product* p = catalog.find_product(cart.cart_ids[i]);
        if (p) s += p->price * cart.cart_qty[i];
      }
      return s;
    }

    double apply_discounts(double subtotal) {
      // silly discount: 10% off if subtotal > $10
      if (subtotal > 10.0) return subtotal * 0.9;
      return subtotal;
    }
};

class Receipt{
public:
    void print_receipt(const Cart& cart, Catalog& catalog, PricingCheckout& pricing) {
        std::cout << "---- Receipt ----\n";
        for (size_t i=0;i<cart.cart_ids.size();++i) {
          Product* p = catalog.find_product(cart.cart_ids[i]);
          if (!p) continue;
          std::cout << p->name << " x" << cart.cart_qty[i] << " @ " << p->price
                    << " = " << (p->price * cart.cart_qty[i]) << "\n";
        }
        double subtotal = pricing.compute_subtotal(cart, catalog);
        double after_discount = pricing.apply_discounts(subtotal);
        double tax = after_discount * pricing.TAX_RATE;
        double total = after_discount + tax;
        std::cout << "Subtotal: " << subtotal << "\n";
        std::cout << "Discounted: " << after_discount << "\n";
        std::cout << "Tax: " << tax << "\n";
        std::cout << "Total: " << total << "\n";
}
};
int main() {
    Catalog myCatalog;
    Cart myCart;
    PricingCheckout myPrice;
    Receipt myReceipt;
    
    myCatalog.load_catalog();

    myCart.add_to_cart(1, 4);   // 4 apples
    myCart.add_to_cart(2, 1);   // 1 bread
    myCart.add_to_cart(4, 3);   // 3 chocolate
    myReceipt.print_receipt(myCart, myCatalog, myPrice);
    return 0;
}
