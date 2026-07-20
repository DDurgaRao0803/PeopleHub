INSERT INTO ServiceCategories
(
    Id,
    Name,
    Description,
    IsActive,
    CreatedOnUtc
)
VALUES
(
    NEWID(),
    'Electrical',
    'Electrical repair and installation services',
    1,
    GETUTCDATE()
),
(
    NEWID(),
    'Plumbing',
    'Plumbing repair and maintenance services',
    1,
    GETUTCDATE()
),
(
    NEWID(),
    'Carpentry',
    'Woodwork and furniture services',
    1,
    GETUTCDATE()
);
GO