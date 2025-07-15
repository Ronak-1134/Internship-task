import { Mission } from "./common.interface";
import { User } from "./user.interface";
export interface MissionTheme {
  missionThemeId?: number;
  title?: string;
  status?: boolean;
}

export interface MissionSkill {
  missionSkillId?: number;
  skillName?: string;
  status?: boolean;
}

export interface MissionApplication {
  missionApplicationId?: number;
  missionId?: number;
  userId?: number;
  appliedDate?: Date;
  approvalStatus?: string;
  missionTitle?: string;
  userName?: string;
}

export interface MissionApplicationApproval {
  id: number,
  missionId: number,
  userId: number,
  appliedDate: Date,
  status: boolean,
  sheet: number,
  mission: Mission,
  user: User
}
