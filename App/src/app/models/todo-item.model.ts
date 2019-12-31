export class TodoItem {
    public description: string;
    public creation_date: Date;
    public finish_date: Date;
    constructor(description: string, creation_date: Date, finishDate: Date = null) {
        this.description = description;
        this.creation_date = creation_date;
        this.finish_date = finishDate;
    }
}
