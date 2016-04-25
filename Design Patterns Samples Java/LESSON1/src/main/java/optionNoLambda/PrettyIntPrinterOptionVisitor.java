/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package optionNoLambda;

/**
 *
 * @author busal
 */
public class PrettyIntPrinterOptionVisitor implements IOptionVisitor<Integer, String> {
    public String onNone()
    {
        return "I am nothing ...";
    }
    
    public String onSome(Integer value)
    {
        return value.toString();
    }
}
