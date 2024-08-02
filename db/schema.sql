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

create table vacunaciones
(
    id            serial primary key,
    id_animal     integer not null,
    name          text    not null,
    date          date    not null,
    examen_previo text,

    foreign key (id_animal) references animals (id) on delete cascade
);

create table desparasitaciones
(
    id        serial primary key,
    id_animal integer not null,
    tipo      text    not null,
    fecha     date    not null,
    producto  text    not null,
    peso      decimal not null,
    formato   text    not null,

    foreign key (id_animal) references animals (id) on delete cascade
);

create table aseos
(
    id        serial primary key,
    id_animal integer not null,
    tipo      text    not null,
    fecha     date    not null,

    foreign key (id_animal) references animals (id) on delete cascade
);

create table pesos
(
    id          serial primary key,
    id_animal   integer not null,
    peso_actual decimal not null,
    fecha       date    not null,

    foreign key (id_animal) references animals (id) on delete cascade
);

create table inventory
(
    id              serial primary key,
    name            text not null,
    main_ingredient text not null,
    -- Validate format
    format          text not null,
    -- Validate volume
    volume          text not null,
    -- Validate via
    via             text not null,
    expiration_date date not null,
    laboratory      text not null,
    origin          text not null,
    -- Validate status
    status          text not null,
    entry_date      date not null,
    box_id          text not null,
    -- Should be an int?
    stock           text not null,
    kind            text not null
);