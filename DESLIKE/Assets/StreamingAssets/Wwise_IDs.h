/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
<<<<<<< Updated upstream
        static const AkUniqueID DEAD_OR_ALIVE = 3575957003U;
        static const AkUniqueID SKELETON_ATK = 180084035U;
=======
        static const AkUniqueID BATTLE_END = 461545889U;
        static const AkUniqueID BATTLE_START = 2706291346U;
        static const AkUniqueID SKELETON_ATK = 180084035U;
        static const AkUniqueID SKELETON_DEAD = 3710894717U;
>>>>>>> Stashed changes
        static const AkUniqueID SKELETON_MOVE = 3933512188U;
        static const AkUniqueID TEST_UI = 1896376002U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace DEAD_OR_ALIVE
        {
            static const AkUniqueID GROUP = 3575957003U;

            namespace STATE
            {
                static const AkUniqueID ALIVE = 655265632U;
                static const AkUniqueID DEAD = 2044049779U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace DEAD_OR_ALIVE

    } // namespace STATES

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
<<<<<<< Updated upstream
        static const AkUniqueID SKELETON_ATK_BUS = 2491616377U;
=======
>>>>>>> Stashed changes
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
