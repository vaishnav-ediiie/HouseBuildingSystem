using UnityEngine;
using GridSystemSpace;

namespace BuildSystemSpace
{
    public abstract class ProbState
    {
        protected BuildProbSO parent;

        internal virtual void Init(BuildProbSO parent) => this.parent = parent;
        internal abstract void MovePreviewTo(CellNumber curCellNum, CellNumber lookAtCellNum, PlayerCompForBuildSystem player);
        internal abstract void ConfirmBuild(int floorNumber);
    }
}