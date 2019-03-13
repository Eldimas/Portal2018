export class AdminRole {
    id: number;
    name: string;
    constructor(role?) {
        role = role || {};
        this.id = role.id || 0;
        this.name = role.name || '';
    }
}
