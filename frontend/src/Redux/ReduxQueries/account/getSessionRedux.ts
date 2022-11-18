import { store, GlobalState } from '../../../store'

type Session = {
    accessToken: string;
    refreshToken: string;
}

export default (state: GlobalState = store.getState()): Session | undefined => {
    if (!state.common.session?.accessToken || !state.common.session.refreshToken) return undefined;
    return {
        accessToken: state.common.session.accessToken,
        refreshToken: state.common.session.refreshToken
    }
}
