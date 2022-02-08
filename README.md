http://187.87.242.13:8084/cgi-bin/recordFinder.cgi?action=find&name=AccessControlCardRec&StartTime=1640995200&EndTime=1643673599

dotnet publish --output "C:\custom\publish\directory"
sc.exe create ".NET Retorno Service" binpath="C:\Path\To\App.WindowsService.exe"
sc.exe start ".NET Retorno Service"
sc.exe stop ".NET Retorno Service"
sc.exe delete ".NET Retorno Service"

Arquivo .ini
Campos:
 - CamID / ColetorID
 - URL com Porta
 - Usuario (Opicional)
 - Senha (Opcional)

 Caso o Usuario e a Senha não forem informado utiliza os valores padrões.


TODO:
- Switch no caso de não ter registro ou acesso invalido
- Salvar log em caso de registro na API
- Salvar Log em caso de erro
- Salvar RecNo para impedir multiplos POST do mesmo registro no records.txt
- Ler records.txt e colocar no Camera.OldRecords

[1]{ }[1]

[2]{ }[2]

[3]{ }[3]

[{"RecNo":"123","ColetorID":2},{"RecNo":"456","ColetorID":2}]


