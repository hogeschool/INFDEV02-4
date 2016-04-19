module Chapter1.Week1.v1

open CommonLatex
open LatexDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime

let slides = 
  [
    Section("Introduction")
    SubSection("Lecture topics")
    ItemsBlock
      [
        !"Intro to DEV4"
        !"Coroutines vs State-Machines vs Switch based systems"
      ]
    SubSection("Course goal")
    ItemsBlock
      [
        ! @"Behind every software successful, ot not, software product there is a story..."
        ! @"A story that follows precise steps..how?"
      ]
    SubSection("The story of every software")
    ItemsBlock
      [
        ! @"Software Engeneering tried in the past to study the process behind software development and came out with a structure that is common to all software developement ``stories''."
        ! @"Such process was called software life cycle."
      ]
    SubSection("Software lifecycle")
    ItemsBlock
      [
        ! @"\textbf{Prototyping} an alpha semi-working version of the application, meant just to take confidence with the problem."
        ! @"\textbf{Designing} an ``ivory tower'' code representing a complete, clean, high-level, design of all the envolved entities (and their interactions) of the problem in question."
        ! @"\textbf{Refactoring} a yet another version of the software that includes ``though'' real life considerations. It can cause a lot of pain, since it does not add functionalities to the software, but rather low-level details (related to, for example, the choosen PC architecture) that will make the code harder to read when compared to the previous version."
      ]
    SubSection("Software lifecycle")
    ItemsBlock
      [
        ! @"As we can imagine prototyping is the element that destroys our ivory tower."
        ! @"Every consideration external to the problem in question add noise. Thus, affects readability and so maintainability :("
        Pause
        ! @"What does this mean?"
        Pause
        ! @"Not maintainable sotware means expensive code that is hard to: bug fix, customizea, and takes lots of time to develop."
        Pause
        ! @"What can we do?"
        Pause
        ! @"Unfortunately, there is no ``closed fomula'' that encompass both \texttt{designing} and \texttt{prototyping}."
        Pause
        ! @"But do not worry life is not though as it looks like. There are always solutions for everything..Or almost..."
      ]
    SubSection("Course goal")
    ItemsBlock
      [
        ! @"What we will do in this course is \textbf{introducing you how to implement design code avoiding, or at least minimizing, refactoring. Thus improving code maintainability.}"
        ! @"However, it will take you some time to become expert in this subject. And one course is not at all sufficient. Key elements to master this topic is experience and lots of refactoring ;)"
        ! @"To become wine experts you should have drunk lots of wine and, why not, got drank many times :P"
      ]
    Section("Problem introduction")
    SubSection("Different solutions a given problem")
    ItemsBlock
      [
        ! @".problem description."
        ! @"Coroutines <- goal ..:)" 
        ! @"Switch based system <- what many starters choose (and not only) ..:("
        ! @"State machines <- what we have taught you so far ..:|"
      ]
    Section("Idea")
    ItemsBlock
      [
        ! @"So far we always used abstractions to solve problems: low level problems $\rightarrow$ high level solutions. Can we use it again?"
        ! @"Observations from the previous problem seggust that abstracting/breaking the involved entities into small one, based on functionalities, and later recomposing them so to achieve the desired behavior is a good strategy."
        ! @"Can we push it further, so to capture more cases?"
        Pause
        ! @"Yes, someone (actually it not just one, but lots of people from different backgrounds in CS, but not only) did this already this fo us and called all these ``abstract'' strategies: design patterns"
      ]
    SubSection("Design Patterns seen by the world")
    ItemsBlock
      [
        ! @"[]\begin{table}
	            \centering
	            \begin{tabular}{|r|l|}
		            \hline
		            MATH & composition \\
		            & decomposition \\ \cline{2-2}
		            \hline
		            Sw. Eng. & encapsulation \\
		            & loose coupling \\ \cline{2-2}
		            \hline
		            Industry   & dry, kiss, yagni, \\
		            (``heap'') & solid, etc.\\ \cline{2-2}	     
		            \hline
	            \end{tabular}
            \end{table}"
      ]
    Section("Conclusions")
    ItemsBlock
      [
        ! @"Course overview: the BIG picture + Assignment (a GUI interfacing a file, for example for saving and loading visual settings)"
        ! @"Conclusions (repeat inroduction)"
      ]
    Section("Course structure")
    ItemsBlock
      [
        ! @"Lectures"
        ItemsBlock
          [ 
            ! @"Intro to design patterns (1 lecture) TODAY"
            ! @"Entities construction - Factory  (1 lecture)"
            ! @"Genralizing behaviors - Adapter (1 lecture)"
            ! @"Extending/Composing behaviors - Decorator (1 lecture)"
            ! @"Composing patterns - MVC, MVVM (1 lecture)"
            ! @"Live coding class (1 lecture)"
          ]
        ! @"Assignment"
        ItemsBlock
          [ ! @"Build a GUI application containing interactable buttons."]
      ]

    UML
      [ Package("Shapes", 
                [Interface("Shape",3.0,0.0,0.0,[Operation("draw", [], Some "Void")])
                 Class("Circle", -3.0, -2.0, Some "Shape", [Attribute("Position", "Point")], [])
                 Class("Square", 3.0, -2.0, Some "Shape", [Attribute("Position", "Point")], [])])
        Class("ShapeFactory", -3.5, -5.5, Option.None, [], [Operation("getShape", [], Option.None)])
        Class("Program", 3.5, -5.5, Option.None, [], [Operation("main", [], Option.None)])
        Arrow("ShapeFactory", "Creates", "Shapes")
        Arrow("Program", "Asks", "ShapeFactory")
      ]
  ]