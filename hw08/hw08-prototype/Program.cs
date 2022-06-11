using hw08_prototype;

// origins
CodSquad codSquad = new CodSquad();
CyprinidsSquad cyprinidsSquad = new CyprinidsSquad();
HerringsSquad herringsSquad = new HerringsSquad();
SharkSquad sharkSquad = new SharkSquad();
StingraySquad stingraySquad = new StingraySquad();

Console.WriteLine("Cloning with IMyCloneable:\n");
CodSquad cloneOfcodSquad = codSquad.clone();
CyprinidsSquad cloneOfcyprinidsSquad = cyprinidsSquad.clone();
HerringsSquad cloneOfherringsSquad = herringsSquad.clone();
SharkSquad cloneOfsharkSquad = sharkSquad.clone();
StingraySquad cloneOfstingraySquad = stingraySquad.clone();

output(codSquad, cloneOfcodSquad);
output(cyprinidsSquad, cloneOfcyprinidsSquad);
output(herringsSquad, cloneOfherringsSquad);
output(sharkSquad, cloneOfsharkSquad);
output(stingraySquad, cloneOfstingraySquad);

Console.WriteLine("Cloning with ICloneable:\n");
CodSquad clone2OfcodSquad = (CodSquad)codSquad.Clone();
CyprinidsSquad clone2OfcyprinidsSquad = (CyprinidsSquad)cyprinidsSquad.Clone();
HerringsSquad clone2OfherringsSquad = (HerringsSquad)herringsSquad.Clone();
SharkSquad clone2OfsharkSquad = (SharkSquad)sharkSquad.Clone();
StingraySquad clone2OfstingraySquad = (StingraySquad)stingraySquad.Clone();

output(codSquad, clone2OfcodSquad);
output(cyprinidsSquad, clone2OfcyprinidsSquad);
output(herringsSquad, clone2OfherringsSquad);
output(sharkSquad, clone2OfsharkSquad);
output(stingraySquad, clone2OfstingraySquad);

void output(Fish origin, Fish clone)
{
    Console.WriteLine("Origin: " + origin);
    Console.WriteLine("Clone:  " + clone + "\n");
}