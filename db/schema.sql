create table animals
(
    id        serial primary key,
    name      text    not null,
    age       integer not null,
    gender    text    not null,
    image_url text,
    adopted   boolean not null default false,
    species   text    not null,
    story     text,
    location  text    not null,
    weight    decimal not null,
    code      text
);