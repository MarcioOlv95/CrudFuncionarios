--drop table tbFuncionarioHabilidade
--drop table tbFuncionario
--drop table tbHabilidade

create table tbFuncionario (
	IdFuncionario INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Nome VARCHAR(50) NOT NULL,
	Sobrenome VARCHAR(100) NOT NULL,
	DataNascimento DATETIME NOT NULL,
	Email VARCHAR(150) NULL,
	Sexo CHAR NOT NULL,
	Idade INT NOT NULL,
	Ativo BIT NOT NULL
)

GO

create table tbHabilidade (
	IdHabilidade INT NOT NULL PRIMARY KEY,
	Descricao VARCHAR(50) NOT NULL,
	Ativo BIT NOT NULL
)

GO

INSERT INTO tbHabilidade VALUES (0, 'CSharp', 1);
INSERT INTO tbHabilidade VALUES (1, 'Java', 1)
INSERT INTO tbHabilidade VALUES (2, 'Angular', 1)
INSERT INTO tbHabilidade VALUES (3, 'Sql', 1)
INSERT INTO tbHabilidade VALUES (4, 'Asp', 1)

GO

create table tbFuncionarioHabilidade (
	IdFuncionario INT NOT NULL REFERENCES tbFuncionario(IdFuncionario),
	IdHabilidade INT NOT NULL REFERENCES tbHabilidade(IdHabilidade),
	Ativo BIT NOT NULL
)

GO


CREATE OR ALTER PROCEDURE PRC_GET_FUNCIONARIO
(
	@IdFuncionario INT
)
AS
BEGIN

	IF (@IdFuncionario = 0)
		SELECT a.IdFuncionario as id,
				a.Nome,
				a.Sobrenome,
				a.DataNascimento,
				a.Email,
				a.Sexo,
				a.Idade,
				a.Ativo,
				c.Descricao as Habilidade
		FROM
			tbFuncionario a
			LEFT JOIN tbFuncionarioHabilidade b ON A.IdFuncionario = B.IdFuncionario AND b.Ativo = 1
			LEFT JOIN tbHabilidade c ON c.IdHabilidade = b.IdHabilidade AND c.Ativo = 1
		
	ELSE
		SELECT a.IdFuncionario as id,
				a.Nome,
				a.Sobrenome,
				a.DataNascimento,
				a.Email,
				a.Sexo,
				a.Idade,
				c.Descricao as Habilidade,
				a.Ativo
		FROM
			tbFuncionario a
			LEFT JOIN tbFuncionarioHabilidade b ON A.IdFuncionario = B.IdFuncionario AND b.Ativo = 1
			LEFT JOIN tbHabilidade c ON c.IdHabilidade = b.IdHabilidade AND c.Ativo = 1
		WHERE
			a.IdFuncionario = @IdFuncionario

END

GO

CREATE OR ALTER PROCEDURE PRC_UPDATE_FUNCIONARIO
(
	@IdFuncionario INT,
	@Nome VARCHAR(50),
	@Sobrenome VARCHAR(150),
	@DataNascimento DATETIME, 
	@Email VARCHAR(150),
	@Sexo CHAR,
	@Idade INT,
	@Habilidade VARCHAR(50),
	@Ativo BIT
)
AS
BEGIN

	CREATE TABLE #IdsHabilidade ( Id INT )

	DECLARE @NewId INT
	DECLARE @Item INT

	IF (@IdFuncionario != 0 AND @Ativo = 0)
		UPDATE tbFuncionario SET Ativo = 0 WHERE IdFuncionario = @IdFuncionario
	ELSE IF (@IdFuncionario = 0)
	BEGIN
		INSERT INTO #IdsHabilidade SELECT * FROM STRING_SPLIT(@Habilidade, ',')

		INSERT INTO tbFuncionario
		(
			Nome,
			Sobrenome, 
			DataNascimento,
			Email, 
			Sexo, 
			Idade, 
			Ativo
		)
		VALUES 
		(
			@Nome,
			@Sobrenome, 
			@DataNascimento,
			@Email,
			@Sexo,
			@Idade,
			1
		)
		SELECT @NewId = SCOPE_IDENTITY()

		WHILE EXISTS (SELECT * FROM #IdsHabilidade)
		BEGIN 
			SELECT TOP 1 @Item = Id  FROM #IdsHabilidade

			PRINT @Item

			INSERT INTO tbFuncionarioHabilidade VALUES (@NewId, @Item, 1)

			DELETE FROM #IdsHabilidade WHERE Id = @Item
		END
	END
	ELSE
	BEGIN
		INSERT INTO #IdsHabilidade SELECT * FROM STRING_SPLIT(@Habilidade, ',')

		UPDATE tbFuncionario 
		SET Nome = @Nome,
			SobreNome = @Sobrenome,
			DataNascimento = @DataNascimento,
			Email = @Email,
			Sexo = @Sexo,
			Idade = @Idade,
			Ativo = @Ativo
		WHERE
			idFuncionario = @IdFuncionario

		UPDATE tbFuncionarioHabilidade SET Ativo = 0 WHERE IdFuncionario = @IdFuncionario

		WHILE EXISTS (SELECT * FROM #IdsHabilidade)
		BEGIN 
			SELECT TOP 1 @Item = Id  FROM #IdsHabilidade

			INSERT INTO tbFuncionarioHabilidade VALUES (@IdFuncionario, @Item, 1)

			DELETE FROM #IdsHabilidade WHERE Id = @Item
		END
	END

	DROP TABLE #IdsHabilidade

END
