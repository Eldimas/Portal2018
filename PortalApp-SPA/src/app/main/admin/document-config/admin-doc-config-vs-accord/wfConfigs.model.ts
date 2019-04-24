/* WorkFlow Configuration Model */
export class Ous {
  id: string;
  name: string;
}

export class WfConfigs {
  priority: number; 
  processType: string;
  computed: string;
  editable: boolean;
  hint: string;
  required: boolean;
  ous: [Ous];
  constructor (wfConfigs?){
    this.priority = wfConfigs.priority || 1;
    this.processType = wfConfigs.processType || 'Sender';
    this.computed = wfConfigs.computed || '';
    this.editable = wfConfigs.editable || true;
    this.hint = wfConfigs.hint || '';
    this.required = wfConfigs.required || false;
    this.ous = wfConfigs.ous || ['1', 'spadm'];
  }
}

