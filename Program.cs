using C_ASSIGNMENT_BUILDER.CurrentAssignment;
using C_ASSIGNMENT_BUILDER.Engine.AssignmentBuilder;

/* Her trengs ingenting å endres, utenom TestAssignmentList hvis man velger å lage sin egen liste over oppgaver.*/
/* Her genererer vi en Guide, som skal gi feedback om en oppgave er fullført ved program restart. */
var guide = new Guide();

/* Her sier vi hvilken oppgaveliste som skal brukes. */
var assignments = new AssignmentList();

/* Her går vi gjennom oppgavesettet. Programmet krasjer med vilje on Assert Failure, men også starte på nytt ved en filsave.
    Når programmet starter på nytt blir en ny rapport generert i consol, med feedback på hva assignments som er ferdig, og hva som er neste på listen. */
TestArrayAndListMethods test = new TestArrayAndListMethods();
test.BinarySearch(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 10);
return assignments.Walkthrough(guide);
