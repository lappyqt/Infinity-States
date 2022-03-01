interface IStorageManager {
    saveItemJson(key: string, item: object): void;
    getItemJson(key: string): object;
}

class LocalStorageManager implements IStorageManager {
    public get storage() {
        return localStorage;
    }

    protected checkItemOnNull(item: object) {
        if (typeof(item) != 'object' || item == null) {
            throw new Error('Item is not an object or is equal to null');
        } 
    }

    public saveItemJson(key: string, item: object): void {
        this.checkItemOnNull(item);
        localStorage.setItem(key, JSON.stringify(item));
    }

    public getItemJson(key: string): object {   
        return JSON.parse(localStorage.getItem(key));
    }
}   

class SessionStorageManager extends LocalStorageManager implements IStorageManager {
    override saveItemJson(key: string, item: object): void {
        this.checkItemOnNull(item);
        sessionStorage.setItem(key, JSON.stringify(item));
    }

    override getItemJson(key: string): object {
        return JSON.parse(sessionStorage.getItem(key));
    }
}