This is a simple Microsoft Visual Studio 2019 WPF application to assist in narrowing the list of potential valid Wordle words.
From a graphical perspective this application is crude, UI's are not my skill.
But the application helps to quickly narrow in on the list of potential solutions to the game.
The UI presents a matrix of 6x5 TextBox's to contain 6 rows of 5 letter guesses.
As each guess is entered you can then left mouse click on the letter and mark it as either 
"Match" - that letter is correct in that column for the (presumably) hidden solution.
"ValidWrongSpot" - that letter is valid in the solution but not that column
"Invalid" - that letter is not valid in any column

There is also a TextBox called "Invalid letters" in which you can simply type the list of letters that are invalid rather than having to left mouse click each one
As you fill in details of the guesses, the right TextBox will update with the list of potential solutions.
