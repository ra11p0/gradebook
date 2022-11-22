import { v4 } from "uuid";
import ActionTypes from "../ActionTypes/newEducationCycleFormActionTypes";

export type State = {
    stages: {
        uuid: string,
        subjects: {
            uuid: string,
            guid: string
        }[]
    }[],
    openStages: string[]
}

const defaultState: State = {
    stages: [],
    openStages: []
}

export default (state: State = defaultState, action: { type: ActionTypes, payload: any }): State => {
    switch (action.type) {
        case ActionTypes.AddStage:
            return {
                ...state,
                stages:
                    [
                        ...state.stages,
                        {
                            uuid: v4(),
                            subjects: [],
                            ...action.payload.stage
                        }
                    ]
            }
        case ActionTypes.SetStages:
            return {
                ...state,
                stages:
                    [
                        ...action.payload.stages
                    ]
            }
        case ActionTypes.RemoveStage:
            return {
                ...state,
                stages:
                    [
                        ...state.stages.filter(st => st.uuid != action.payload.uuid)
                    ]
            }
        case ActionTypes.AddSubjectToStage:
            {
                if (!action.payload.subject) return { ...state }
                const stage = state.stages.find(st => st.uuid == action.payload.stageUuid);
                if (!stage) return { ...state };
                const stageIndex = state.stages.indexOf(stage);
                return {
                    ...state,
                    stages:
                        [
                            ...state.stages.filter((st, index) => st.uuid != stage.uuid && index < stageIndex),
                            {
                                ...stage,
                                subjects:
                                    [
                                        ...stage?.subjects ?? [],
                                        action.payload.subject
                                    ]
                            },
                            ...state.stages.filter((st, index) => st.uuid != stage.uuid && index > stageIndex),
                        ]
                }
            }
        case ActionTypes.RemoveSubjectFromStage:
            {
                const stage = state.stages.find(st => st.uuid == action.payload.stageUuid);
                if (!stage) return { ...state };
                const stageIndex = state.stages.indexOf(stage);
                return {
                    ...state,
                    stages:
                        [
                            ...state.stages.filter((st, index) => st.uuid != stage.uuid && index < stageIndex),
                            {
                                ...stage,
                                subjects:
                                    [
                                        ...stage?.subjects.filter(sb => sb.uuid != action.payload.subjectUuid),
                                    ]
                            },
                            ...state.stages.filter((st, index) => st.uuid != stage.uuid && index > stageIndex),
                        ]
                }
            }
        case ActionTypes.SetSubjectGuidForSubjectInStage:
            {
                const stage = state.stages.find(st => st.uuid == action.payload.stageUuid);
                const subject = (stage?.subjects ?? []).find(st => st.uuid == action.payload.subjectUuid);
                if (!stage || !subject) return { ...state };
                const stageIndex = state.stages.indexOf(stage);
                const subjectIndex = stage.subjects.indexOf(subject);
                return {
                    ...state,
                    stages:
                        [
                            ...state.stages.filter((st, index) => st.uuid != stage.uuid && index < stageIndex),
                            {
                                ...stage,
                                subjects:
                                    [
                                        ...(stage?.subjects ?? []).filter((st, index) => st.uuid != action.payload.subjectUuid && index < subjectIndex),
                                        {
                                            ...subject,
                                            guid: action.payload.subjectGuid
                                        },
                                        ...(stage?.subjects ?? []).filter((st, index) => st.uuid != action.payload.subjectUuid && index > subjectIndex),

                                    ]
                            },
                            ...state.stages.filter((st, index) => st.uuid != stage.uuid && index > stageIndex),
                        ]
                }
            }
        case ActionTypes.SetOpenStages:
            return {
                ...state,
                openStages: action.payload.open
            }
        default: return state
    }
}
