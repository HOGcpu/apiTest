Navodila za zagon aplikacije:

1. Prenesite vse datoteke iz priloženega .zip arhiva.
2. Preverite, da imate nameščene .NET SDK (različica 6 ali novejša), Serilog 
   in ostale SDK-je za backend del aplikacije.
3. Backend(C#):
   - Odprite mapo "projectAPIBackend" v Visual Studio ali Visual Studio Code.
   - Build oziroma zaženite aplikacijo (npr. z ukazom "dotnet run" v terminalu VS Code).
   - Aplikacija bo zagnana na fiksnem naslovu http://localhost:5050.
4. Frontend:
   - Odprite mapo "projectAPIFrontend" in dvojni klik na "index.html".

Za pošiljanje sporočil bo frontend komuniciral z backendom prek API klicev na 
"http://localhost:5050/api/message".
