/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package number;

/**
 *
 * @author busal
 */
public interface INumberVisitor {   
    void visit(MyFloat number);

    void visit(MyInt number);
}
