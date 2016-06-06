
import example1.Filter;
import example1.*;
import example2.*;
import iterator1.safeCollections.Iterator;
import iterator1.safeCollections.NaturalList;
import sun.reflect.generics.reflectiveObjects.NotImplementedException;

class Program {

    public interface I {

        void X();
    }

    abstract class A implements I {

        @Override
        public abstract void X();

        public void Test() {
            System.out.println("A");
        }
    }

    class B extends A {

        @Override
        public void Test() {
            super.Test();
            System.out.println("B");
        }

        @Override
        public void X() {
            throw new NotImplementedException();
        }
    }

    public static void main(String[] args) {

//        B b = new B();
//        b.Test();
        ///Example 1 test
        //Iterator<int> iterator = new Lesson2.SafeCollections.NaturalList();
        //Iterator<int> iterator = new Filter<int>(new Lesson2.SafeCollections.NaturalList(), value => value % 2 == 0);
        
        Iterator<String> iterator = new Map<Integer, String>(new Filter<Integer>(new NaturalList(), value -> value % 2 == 0), elem -> elem + " :)");

        for (int i = 0; i < 10; i++) {
            iterator.getNext().visit(() -> {
                System.out.println("Done");
                return null;
            }, v -> {
                System.out.println(v);
                return null;
            });
        }

        ///Example 2 test
        System.out.println("***\r\nBegin program\r\n");

        //IRepository<Customer> customerRepository = new Repository<Customer>();
        IRepository<Customer> customerRepository = new LoggerRepository<Customer>(new Repository<Customer>());

        Customer customer = new Customer(1, "Customer 1", "Address 1");

        customerRepository.add(customer);
        customerRepository.update(customer);
        customerRepository.delete(customer);

        System.out.println("\r\nEnd program \r\n***");

    }
}
