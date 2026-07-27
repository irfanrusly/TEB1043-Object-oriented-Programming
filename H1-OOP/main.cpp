#include <iostream>
using namespace std;

void Deposit(float amount, float &balance){
    if (amount > 0){
        balance += amount;
    }
    else{
        cout << "Invalid deposit amount." << endl
    }
   
}
bool Withdraw(float amount, float &balance){
    if(amount > 0 && balance >= amount){
        balance -= amount;
        return true;
    }
    else{
        cout << "Insufficient fund" << endl;
        return false;
    }
    
}
int main(void){
    float currentBalance = 2000;
    
    cout << "Your current balance:" << currentBalance << endl;
    Deposit(2000.0, currentBalance);
    cout << "After deposited:" << endl;
    cout << "Your current balance:" << currentBalance << endl;
    cout << "After withdrawing" << endl;
    Withdraw(500, currentBalance);
    cout << "Your current balance:" << currentBalance << endl;
    return 0;


}
