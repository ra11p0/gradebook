import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { store } from "../../store";

export default class HubProxy {
    private subscribers: Subscriber[] = [];
    private connection: HubConnection | undefined = undefined;
    public connect(url: string) {
        if (this.connection) this.connection.stop();
        this.connection = new HubConnectionBuilder()
            .withUrl(url, { accessTokenFactory: () => store.getState().common.session.accessToken })
            .withAutomaticReconnect()
            .build();

        this.connection!.start().then(() => {
            this.subscribers.forEach(s => { this.connection?.on(s.methodName, s.newMethod) })
        });
    }
    public send(method: string, content: any) {
        this.connection?.send(method, content);
    }
    public on(methodName: string, newMethod: (...args: any[]) => any) {
        this.subscribers.push({ methodName, newMethod })
        this.connection?.on(methodName, newMethod);
    }
    public off(methodName: string) {
        this.subscribers = this.subscribers.filter(e => e.methodName != methodName);
        this.connection?.off(methodName);
    }
}

export interface Subscriber { methodName: string, newMethod: (...args: any[]) => any };

