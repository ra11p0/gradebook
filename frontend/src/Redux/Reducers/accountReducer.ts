import SchoolRolesEnum from '../../Common/Enums/SchoolRolesEnum';
import ActionType from '../ActionTypes/accountActionTypes';

export interface State {
  appLoaded: boolean;
  school?: {
    schoolGuid: string;
    schoolName: string;
  };
  user?: {
    userId: string;
  };
  person?: {
    personGuid: string;
  };
  session?: {
    accessToken: string;
    refreshToken: string;
  };
  schoolsList?: Array<{
    person: {
      guid: string;
      name: string;
      surname: string;
      schoolRole: SchoolRolesEnum;
      schoolGuid: string;
      activeClassGuid: string;
      birthday: Date;
      activeClass: string;
    };
  }>;
  permissions?: any;
  language?: string;
}

const defaultState = {
  appLoaded: false,
};

export default (
  state: State = defaultState,
  action: { type: ActionType; payload: any }
): State => {
  switch (action.type) {
    case ActionType.AppLoad:
      return {
        ...state,
        appLoaded: action.payload.isAppLoaded,
      };
    case ActionType.SetUser:
      return {
        ...state,
        user: {
          userId: action.payload.userId,
        },
      };
    case ActionType.SetPerson:
      return {
        ...state,
        person: {
          personGuid: action.payload.personGuid,
        },
      };
    case ActionType.LogIn:
      return {
        ...state,
        session: {
          accessToken: action.payload.accessToken,
          refreshToken: action.payload.refreshToken,
        },
      };
    case ActionType.LogOut:
      return {
        ...state,
        session: undefined,
        school: undefined,
        schoolsList: undefined,
        user: undefined,
        permissions: undefined,
        person: undefined,
      };
    case ActionType.SetSchool:
      return {
        ...state,
        school: {
          ...state.school,
          schoolGuid: action.payload.schoolGuid,
          schoolName: action.payload.schoolName,
        },
      };
    case ActionType.SetSchoolsList:
      return {
        ...state,
        schoolsList: action.payload.schoolsList,
      };
    case ActionType.SetPermissions:
      return {
        ...state,
        permissions: action.payload.permissions,
      };
    case ActionType.SetLanguage:
      return {
        ...state,
        language: action.payload.language,
      };
    default:
      return state;
  }
};
